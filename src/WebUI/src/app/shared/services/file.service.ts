import { HttpClient, HttpHeaders } from '@angular/common/http';
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
        return this.http.put(signedURL, file, { headers, reportProgress: true, observe: 'events' });
    }
  

    getS3PresignedUrl(filename: string, type: string) : Observable<any> {        
        return this.http.post(this._filesURL + 'get_upload_url', { Filename: filename, Type: type });
    }
       
}