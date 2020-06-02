import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartPageComponent } from './pages/cart-page/cart-page.component';
import { CartRoutingModule } from './cart-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    declarations: [
        CartPageComponent
    ],
    imports: [ 
        CommonModule,
        CartRoutingModule,
        SharedModule
    ],
    exports: [],
    providers: [   
    ],
})
export class CartModule {}