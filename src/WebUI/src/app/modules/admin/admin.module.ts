import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { ConcertOverviewPageComponent } from './pages/concert-overview-page/concert-overview-page.component';
import { ConcertCreatePageComponent } from './pages/concert-create-page/concert-create-page.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedModule } from 'src/app/shared/shared.module';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { VenueCreateModalComponent } from './components/venue-create-modal/venue-create-modal.component';

@NgModule({
    declarations: [
        ConcertOverviewPageComponent,
        ConcertCreatePageComponent,
        VenueCreateModalComponent
    ],
    imports: [ 
        CommonModule,
        AdminRoutingModule,
        RouterModule,
        FormsModule,
        ReactiveFormsModule,
        NgxDatatableModule,
        SharedModule,
        NgbModalModule,
        NgSelectModule       
     ],
    exports: [],
    providers: [],
    entryComponents: [
        VenueCreateModalComponent
    ]
})
export class AdminModule {}