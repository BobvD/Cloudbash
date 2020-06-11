import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ConcertSinglePageComponent } from './pages/concert-single-page/concert-single-page.component';
import { MainLayoutComponent } from 'src/app/shared/layouts/main-layout/main-layout.component';
import { ConcertResolver } from './services/concert-resolver.service';
import { ConcertSearchPageComponent } from './pages/concert-search-page/concert-search-page.component';


const routes: Routes = [
   
    {
        path: 'concert',
        component: MainLayoutComponent,
        children: [            
            { 
                path: 'search', 
                component: ConcertSearchPageComponent
            },
            { 
                path: ':id', 
                component: ConcertSinglePageComponent,
                resolve: { concert: ConcertResolver }
            }
        ]
    },
];

@NgModule({
    imports: [CommonModule, RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ConcertRoutingModule { }