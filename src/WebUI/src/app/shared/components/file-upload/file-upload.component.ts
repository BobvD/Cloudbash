import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { FileService } from '../../services/file.service';

@Component({
    selector: 'app-file-upload',
    templateUrl: './file-upload.component.html',
    styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent implements OnInit {
    @ViewChild('formEl', { static: false }) formEl: ElementRef;
    public form: FormGroup;
    public selectedFile: File;    


    constructor(
        private formBuilder: FormBuilder,
        private fileService: FileService
      ) {}


      ngOnInit() {
        this.createForm();       
      }
    
     
      public onFilesSelection($event: Event): void {
        console.log($event);
        const target = $event.target || $event.srcElement;
        this.selectedFile = target[`files`][0];
      }
    
    
      createForm() {
        this.form = this.formBuilder.group({
          fileUpload: [null, Validators.required],
        });
      }


      onSubmit() {
        this.fileService.getS3PresignedUrl(this.selectedFile.name, this.selectedFile.type).subscribe(url => {
            console.log(url);
            console.log(this.selectedFile);

            this.fileService.uploadFileToS3(this.selectedFile, url).subscribe(res => {
                console.log(res);
            })

        })
      }
    
}
