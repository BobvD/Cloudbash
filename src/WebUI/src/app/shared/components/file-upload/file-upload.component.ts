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
        this.fileService.getS3PresignedUrl(this.fileToUpload.name, this.fileToUpload.type).subscribe(url => {
            this.fileService.uploadFileToS3(this.fileToUpload, url).subscribe(
                (res) => this.uploadResponse = res,
                (err) => this.error = err
            );
        })
    }

}
