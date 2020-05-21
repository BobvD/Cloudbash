import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ConcertOverviewPageComponent } from './pages/concert-overview-page/concert-overview-page.component';
import { ConcertCreatePageComponent } from './pages/concert-create-page/concert-create-page.component';
import { DashboardPageComponent } from './pages/dashboard-page/dashboard-page.component';


const routes: Routes = [
    { path: '', component: DashboardPageComponent, pathMatch: 'full' },
    { path: 'concerts', component: ConcertOverviewPageComponent, pathMatch: 'full' },
    { path: 'concerts/new', component: ConcertCreatePageComponent, pathMatch: 'full' }
];

@NgModule({
    imports: [CommonModule, RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminRoutingModule { }