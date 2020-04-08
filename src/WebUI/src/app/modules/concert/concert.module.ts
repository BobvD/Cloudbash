import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConcertGridComponent } from './components/product-grid/concert-grid.component';
import { ConcertGridItemComponent } from './components/concert-grid-item/concert-grid-item.component';

@NgModule({
    declarations: [
        ConcertGridComponent,
        ConcertGridItemComponent
    ],
    imports: [ CommonModule ],
    exports: [
        ConcertGridComponent
    ],
    providers: [],
})
export class ConcertModule {}