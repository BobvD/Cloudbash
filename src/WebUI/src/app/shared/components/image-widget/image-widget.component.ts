import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { MediaService } from 'src/app/shared/services/media.service';

@Component({
    selector: 'app-image-widget',
    templateUrl: './image-widget.component.html',
    styleUrls: ['./image-widget.component.scss']
})
export class ImageWidgetComponent implements OnInit {
    @Input() images: string[];
    @Output() imageResult = new EventEmitter<any>();
  
    imageChangedEvent: any = '';
      croppedImage: any = '';
      aspectRatio = 16 / 9;
      imageBase64;
      imageFile;
  
      constructor(private mediaService: MediaService) { }
  
      ngOnInit(): void {
          this.setImageInCropper(this.images[0]);
      }
  
      fileChangeEvent(event: any): void {
          this.imageChangedEvent = event;
      }
  
      imageCropped(event: ImageCroppedEvent) {
          this.croppedImage = event.base64;
          // this.imageFile = event.file;
          this.imageResult.emit(this.b64toBlob(this.croppedImage));
      }
  
      cropImage() {
          this.imageBase64 = this.croppedImage;
      }
  
      imageLoaded() {
          // show cropper
      }
      cropperReady() {
          // cropper ready
      }
      loadImageFailed() {
          // show message
      } 
  
      setImageInCropper(imageUrl: string) {
        console.log('LOAD IMAGE')
          this.mediaService.getImageFromUrl(imageUrl).subscribe(res => {
            console.log('LOAD IMAGE')
            console.log(res)
              var reader = new FileReader();
              reader.readAsDataURL(res);
              return reader.onloadend = () => {
                  this.imageBase64 = reader.result;
              }
          })
      }
    
      b64toBlob(dataURI) {
          var byteString = atob(dataURI.split(',')[1]);
          var ab = new ArrayBuffer(byteString.length);
          var ia = new Uint8Array(ab);
  
          for (var i = 0; i < byteString.length; i++) {
              ia[i] = byteString.charCodeAt(i);
          }
          return new Blob([ab], { type: 'image/jpeg' });
      }
}
