import {
    animate,
    keyframes,
    state,
    style,
    transition,
    trigger
  } from '@angular/animations';
  import { Component } from '@angular/core';
  
  import { Toast, ToastrService, ToastPackage } from 'ngx-toastr';
  
  @Component({
    selector: '[pink-toast-component]',
    templateUrl: './info-box.component.html',
    styleUrls: ['./info-box.component.scss'],
    animations: [],
    preserveWhitespaces: false,
  })
  export class InfoBoxComponent extends Toast {
    // used for demo purposes
    undoString = 'undo';
  
    // constructor is only necessary when not using AoT
    constructor(
      protected toastrService: ToastrService,
      public toastPackage: ToastPackage,
    ) {
      super(toastrService, toastPackage);
    }
  
    action(event: Event) {
      event.stopPropagation();
      this.undoString = 'undid';
      this.toastPackage.triggerAction();
      return false;
    }
  }