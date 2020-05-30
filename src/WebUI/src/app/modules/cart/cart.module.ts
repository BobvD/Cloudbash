import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartPageComponent } from './pages/cart-page/cart-page.component';
import { CartRoutingModule } from './cart-routing.module';
import { CartResolver } from './services/cart-resolver.service';

@NgModule({
    declarations: [
        CartPageComponent
    ],
    imports: [ 
        CommonModule,
        CartRoutingModule 
    ],
    exports: [],
    providers: [
        CartResolver        
    ],
})
export class CartModule {}