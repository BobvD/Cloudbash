import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// 3rd party
import { NgbDropdownModule, NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';

// Components
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FormsModule } from '@angular/forms';

@NgModule({
    declarations: [
        HeaderComponent,
        FooterComponent,
        NavbarComponent
    ],
    imports: [ 
        CommonModule,
        NgbDropdownModule,
        NgbCollapseModule,
        FormsModule
     ],
    exports: [
        HeaderComponent,
        FooterComponent

    ],
    providers: [],
})
export class SharedModule {}