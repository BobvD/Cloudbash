import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

// 3rd party
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ArchwizardModule } from 'angular-archwizard';

// Modules
import { SharedModule } from 'src/app/shared/shared.module';
import { AdminRoutingModule } from './admin-routing.module';

// Components
import { ConcertOverviewPageComponent } from './pages/concert-overview-page/concert-overview-page.component';
import { ConcertCreatePageComponent } from './pages/concert-create-page/concert-create-page.component';
import { VenueCreateModalComponent } from './components/venue-create-modal/venue-create-modal.component';
import { TicketTypeCreateModalComponent } from './components/ticket-type-create-modal/ticket-type-create-modal.component';
import { DashboardPageComponent } from './pages/dashboard-page/dashboard-page.component';
import { ConcertDetailsFormComponent } from './components/concert-details-form/concert-details-form.component';
import { ConcertTicketsFormComponent } from './components/concert-tickets-form/concert-tickets-form.component';
import { ConcertDatetimeFormComponent } from './components/concert-datetime-form/concert-datetime-form.component';

@NgModule({
    declarations: [
        ConcertOverviewPageComponent,
        ConcertCreatePageComponent,
        VenueCreateModalComponent,
        TicketTypeCreateModalComponent,
        DashboardPageComponent,
        ConcertDetailsFormComponent,
        ConcertTicketsFormComponent,
        ConcertDatetimeFormComponent
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
        NgSelectModule,
        ArchwizardModule       
     ],
    exports: [],
    providers: [],
    entryComponents: [
        VenueCreateModalComponent,
        TicketTypeCreateModalComponent
    ]
})
export class AdminModule {}