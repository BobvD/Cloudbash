import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { ConcertOverviewPageComponent } from './pages/concert-overview-page/concert-overview-page.component';
import { ConcertCreatePageComponent } from './pages/concert-create-page/concert-create-page.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
    declarations: [
        ConcertOverviewPageComponent,
        ConcertCreatePageComponent
    ],
    imports: [ 
        CommonModule,
        AdminRoutingModule,
        RouterModule,
        FormsModule,
        ReactiveFormsModule,
        NgxDatatableModule,
        SharedModule        
     ],
    exports: [],
    providers: [],
})
export class AdminModule {}