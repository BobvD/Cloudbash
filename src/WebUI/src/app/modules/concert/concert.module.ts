import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConcertGridComponent } from './components/product-grid/concert-grid.component';
import { ConcertGridItemComponent } from './components/concert-grid-item/concert-grid-item.component';
import { ConcertSinglePageComponent } from './pages/concert-single-page/concert-single-page.component';
import { ConcertRoutingModule } from './concert-routing.module';
import { ConcertResolver } from './services/concert-resolver.service';
import { SharedModule } from 'src/app/shared/shared.module';
import { ConcertSearchPageComponent } from './pages/concert-search-page/concert-search-page.component';

@NgModule({
    declarations: [
        ConcertGridComponent,
        ConcertGridItemComponent,
        ConcertSinglePageComponent,
        ConcertSearchPageComponent
    ],
    imports: [ 
        CommonModule,
        ConcertRoutingModule,
        SharedModule
     ],
    exports: [
        ConcertGridComponent
    ],
    providers: [
        ConcertResolver
    ],
})
export class ConcertModule {}