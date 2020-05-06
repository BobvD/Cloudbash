import { HttpClient, HttpHeaders, HttpEventType } from '@angular/common/http';
import { map } from 'rxjs/internal/operators/map';
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';

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
        
        console.log('Trying to upload');
        return this.http.put(signedURL, file, { headers, reportProgress: true, observe: 'events' }) 
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
        return this.http.post(this._filesURL + 'get_upload_url', { Filename: filename, Type: type });
    }
       
}