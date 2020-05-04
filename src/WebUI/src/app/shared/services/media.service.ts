import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/internal/operators/map';
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';

@Injectable()
export class MediaService {
    private _filesURL: string;

    constructor(private http: HttpClient) {
        this._filesURL = localStorage.getItem('apiUrl') + '/files';
    }


    upload(title: string, blob: any) {
        const formData = new FormData();
        formData.append('file', blob, title);
        return this.http.post(this._filesURL,
            formData, { reportProgress: true, observe: 'events' }).pipe(map(res => {
                return res;
            }));
    }

    getImageFromUrl(imageUrl: string): Observable<any> {
        return this.http.get(imageUrl, { responseType: "blob" });
    }

    getBase64FromBlob(blob: any) {
        var reader = new FileReader();
        reader.readAsDataURL(blob);
        return reader.onloadend = () => {
            return reader.result;
        }
    }

    getBlobFromBase64(base64: string, name: string) {
        // Naming the image
        const date = new Date().valueOf();
        // Replace extension according to your media type
        const imageName = date + '.jpg';
        // call method that creates a blob from dataUri
        const imageBlob = this.dataURItoBlob(base64);
        return new File([imageBlob], imageName, { type: 'image/jpeg' });
    }

    dataURItoBlob(dataURI) {
        const byteString = window.atob(dataURI);
        const arrayBuffer = new ArrayBuffer(byteString.length);
        const int8Array = new Uint8Array(arrayBuffer);
        for (let i = 0; i < byteString.length; i++) {
            int8Array[i] = byteString.charCodeAt(i);
        }
        const blob = new Blob([int8Array], { type: 'image/jpeg' });
        return blob;
    }

}