
  import {
    trigger
  } from '@angular/animations';

  import { Component } from '@angular/core';
  
  import { Toast } from 'ngx-toastr';
  
  @Component({
    selector: '[pink-toast-component]',
    templateUrl: './info-box.component.html',
    styleUrls: ['./info-box.component.scss'],
    animations: [
      trigger('flyInOut', []),
    ],
    preserveWhitespaces: false,
  })
  export class InfoBoxComponent extends Toast {
   
  
  
  }