import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ConcertOverviewPageComponent } from './pages/concert-overview-page/concert-overview-page.component';
import { ConcertCreatePageComponent } from './pages/concert-create-page/concert-create-page.component';


const routes: Routes = [
    { path: 'concerts', component: ConcertOverviewPageComponent },
    { path: 'concerts/new', component: ConcertCreatePageComponent }
];

@NgModule({
    imports: [CommonModule, RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminRoutingModule { }