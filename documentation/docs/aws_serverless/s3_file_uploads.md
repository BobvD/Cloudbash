# Upload files to Amazon Simple Storage Service (S3) with Presigned Urls

## Client-side code
**file.service.ts** - [source](https://github.com/BobvD/Cloudbash/blob/dev/src/WebUI/src/app/shared/services/file.service.ts) <br />
``` typescript
@Injectable()
export class FileService {
    private _filesURL: string;
  
    constructor(private http: HttpClient) {
        this._filesURL = localStorage.getItem('apiUrl') + '/files/';
    }

    public uploadFileToS3(file: File, signedURL: string): Observable<any> {
        
        const headers = new HttpHeaders();
        headers.append('Content-Type', file.type);
        headers.append("x-amz-acl", 'public-read');
        
        return this.http.put(signedURL, file, 
                                { headers, reportProgress: true, observe: 'events' }) 
        .pipe(map((event) => {
            switch (event.type) {
      
              case HttpEventType.UploadProgress:
                const progress = Math.round(100 * event.loaded / event.total);
                return { status: 'progress', message: progress };
      
              case HttpEventType.Response:
                return event.body;
              default:
                return `Unhandled event: ${event.type}`;
            }            
        }))
    }

    getS3PresignedUrl(filename: string, type: string) : Observable<any> {        
        return this.http.post(this._filesURL + 'get_upload_url', 
                                { Filename: filename, Type: type });
    }       

}
```
**file-upload.component.ts** - [source](https://github.com/BobvD/Cloudbash/blob/dev/src/WebUI/src/app/shared/components/file-upload/file-upload.component.ts) <br />
``` typescript
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FileService } from '../../services/file.service';

@Component({
    selector: 'app-file-upload',
    templateUrl: './file-upload.component.html',
    styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent implements OnInit {
    @ViewChild('formEl', { static: false }) formEl: ElementRef;
    public form: FormGroup;
    public fileToUpload: File;

    uploadResponse = { status: '', message: '', filePath: '' };
    error: string;

    constructor(
        private formBuilder: FormBuilder,
        private fileService: FileService) { }

    ngOnInit() {
        this.createForm();
    }

    createForm() {
        this.form = this.formBuilder.group({
            fileUpload: [null, Validators.required],
        });
    }

    public onFilesSelection($event: Event): void {
        const target = $event.target || $event.srcElement;
        this.fileToUpload = target[`files`][0];
    }

    onSubmit() {
        this.fileService.getS3PresignedUrl(this.fileToUpload.name, this.fileToUpload.type)
        .subscribe(url => {
            this.fileService.uploadFileToS3(this.fileToUpload, url).subscribe(
                (res) => this.uploadResponse = res,
                (err) => this.error = err
            );
        })
    }

}

```
**file-upload.component.html** - [source](https://github.com/BobvD/Cloudbash/blob/dev/src/WebUI/src/app/shared/components/file-upload/file-upload.component.html) <br />
``` html
<form #formEl novalidate (ngSubmit)="onSubmit()" [formGroup]="form" class="d-flex">
    <div>
        <input id="fileUpload" name="fileUpload" type="file"  class="mr-2"
            [formControlName]="'fileUpload'" (change)="onFilesSelection($event)" 
            accept="image/*" />
    </div>
    <div>
        <button type="submit" class="btn btn-secondary">
            Upload image
        </button>
    </div>
</form>

<div *ngIf="uploadResponse.status === 'progress'" class="progress mt-2">
    <div class="progress-bar progress-bar-striped" role="progressbar"
        [style.width.%]="uploadResponse.message" 
        aria-valuenow="25" aria-valuemin="0" aria-valuemax="100">
        {{uploadResponse.message}}%
    </div>
</div>
```
**GetS3PreSignedUrlFunction.cs** - [source](https://github.com/BobvD/Cloudbash/blob/dev/src/Lambda/Functions/Files/GetS3PreSignedURLFunction.cs) <br />
``` csharp
[LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
{           
    var requestModel = 
        JsonConvert.DeserializeObject<GetS3PreSignedUrlCommand>(request.Body);
    
    try
    {               
        var result = await Mediator.Send(requestModel);
        return new APIGatewayProxyResponse
        {
            Headers = GetCorsHeaders(),
            StatusCode = 201,
            Body = JsonConvert.SerializeObject(result)
        };
    }
    catch (Exception ex)
    {
        return new APIGatewayProxyResponse
        {
            Headers = GetCorsHeaders(),
            StatusCode = 400,
            Body = JsonConvert.SerializeObject(ex.Message)
        };
    }
}
```
## Server-side code
**GetS3PreSignedUrlCommand.cs** - [source](https://github.com/BobvD/Cloudbash/blob/dev/src/Application/Files/Commands/GenerateS3PreSignedUrl/GetS3PreSignedUrlCommand.cs) <br /> 
``` csharp
public class GetS3PreSignedUrlCommand : IRequest<string>
{
    public string Filename { get; set; }
    public string Type { get; set; }

    public class GetS3PreSignedUrlCommandHandler 
        : IRequestHandler<GetS3PreSignedUrlCommand, string>
    {

        private IFileService _fileService;

        public GetS3PreSignedUrlCommandHandler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<string> Handle(GetS3PreSignedUrlCommand request, 
                                            CancellationToken cancellationToken)
        {
            return _fileService.GetUploadUrl(request.Filename, request.Type);
        }
    }
}
```
**S3FileService.cs** - [source](https://github.com/BobvD/Cloudbash/blob/dev/src/Infrastructure/Services/S3FileService.cs) <br />
``` csharp
using Amazon.S3;
using Amazon.S3.Model;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Infrastructure.Configs;
using System;

namespace Cloudbash.Infrastructure.Services
{
    public class S3FileService : IFileService
    {
        private readonly AmazonS3Client _amazonS3Client;
        private readonly IServerlessConfiguration _config;

        public S3FileService(IAwsClientFactory<AmazonS3Client> clientFactory,
                                  IServerlessConfiguration config)
        {
            _amazonS3Client = clientFactory.GetAwsClient();
            _config = config;
        }

        public string GetUploadUrl(string filename, string type)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _config.BucketName,
                Key = filename,
                Verb = HttpVerb.PUT,
                ContentType = type,
                Expires = DateTime.Now.AddMinutes(15)
            };

            return _amazonS3Client.GetPreSignedURL(request);           
        }
    }
}
```