
import {
  trigger
} from '@angular/animations';
import { Component, OnInit, NgZone } from '@angular/core';  
import { Toast, ToastPackage, ToastrService } from 'ngx-toastr';
import { ConfigService } from '../../services/config.service';
import { Config } from '../../models/config.model';
  
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
    selectedConfig: Config;
    configs = [];
    showAlert =  false;

    constructor(private configService: ConfigService,
                toastrService: ToastrService, 
                toastPackage: ToastPackage, 
                ngZone?: NgZone){
      super(toastrService, toastPackage, ngZone);
    }

    ngOnInit(): void {
      this.selectedConfig = this.configService.getConfigFromStorage();
      this.configs = this.configService.getAllConfigs();    
    }

    onConfigChange($event) {
      this.configService.setConfig(this.selectedConfig.id);
      this.showAlert = true;
    } 
    
    saveApiUrl() {
      this.configService.setApiUrl(this.selectedConfig.apiUrl);
    }
  }