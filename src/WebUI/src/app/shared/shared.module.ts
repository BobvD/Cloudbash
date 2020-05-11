import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

// 3rd party
import { NgbDropdownModule, NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerModule } from "ngx-spinner";
import { ImageCropperModule } from 'ngx-image-cropper';

// Services
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { FileService } from './services/file.service';

// Components
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { InfoBoxComponent } from './components/info-box/info-box.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { FileUploadComponent } from './components/file-upload/file-upload.component';


@NgModule({
    declarations: [
        HeaderComponent,
        FooterComponent,
        NavbarComponent,
        AuthLayoutComponent,
        InfoBoxComponent,
        AdminLayoutComponent,
        FileUploadComponent
    ],
    imports: [ 
        CommonModule,
        NgbDropdownModule,
        NgbCollapseModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,        
        NgxSpinnerModule,
        ImageCropperModule
     ],
    exports: [
        HeaderComponent,
        FooterComponent,
        FileUploadComponent
    ],
    providers: [
        FileService,
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
    ],
})
export class SharedModule {}