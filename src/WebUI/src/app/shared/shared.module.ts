import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// 3rd party
import { NgbDropdownModule, NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';

// Components
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { InfoBoxComponent } from './components/info-box/info-box.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';

@NgModule({
    declarations: [
        HeaderComponent,
        FooterComponent,
        NavbarComponent,
        AuthLayoutComponent,
        InfoBoxComponent,
        AdminLayoutComponent
    ],
    imports: [ 
        CommonModule,
        NgbDropdownModule,
        NgbCollapseModule,
        FormsModule,
        RouterModule
     ],
    exports: [
        HeaderComponent,
        FooterComponent

    ],
    providers: [],
})
export class SharedModule {}