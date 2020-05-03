
  import {
    trigger
  } from '@angular/animations';

  import { Component, OnInit } from '@angular/core';
  
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
  export class InfoBoxComponent extends Toast implements OnInit {
   
    apiUrl: string = "";

    ngOnInit(): void {
      this.apiUrl = localStorage.getItem('apiUrl');
      console.log(this.apiUrl);      
    }
    
    saveApiUrl() {
      localStorage.setItem('apiUrl', this.apiUrl);
    }
  }