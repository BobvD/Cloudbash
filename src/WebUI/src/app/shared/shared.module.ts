import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// Components
import { HomePageComponent } from './pages/home-page/home-page.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavbarComponent } from './components/navbar/navbar.component';

@NgModule({
    declarations: [
        HomePageComponent,
        HeaderComponent,
        FooterComponent,
        NavbarComponent
    ],
    imports: [ CommonModule ],
    exports: [
        HomePageComponent,
        HeaderComponent,
        FooterComponent

    ],
    providers: [],
})
export class SharedModule {}