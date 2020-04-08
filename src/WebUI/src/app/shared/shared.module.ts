import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// 3rd party
import { NgbDropdownModule, NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';

// Components
import { HomePageComponent } from './pages/home-page/home-page.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FormsModule } from '@angular/forms';

@NgModule({
    declarations: [
        HomePageComponent,
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
        HomePageComponent,
        HeaderComponent,
        FooterComponent

    ],
    providers: [],
})
export class SharedModule {}