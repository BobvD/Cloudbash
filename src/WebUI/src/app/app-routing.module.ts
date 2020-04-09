
import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { HomePageComponent } from './shared/pages/home-page/home-page.component';

const routes: Routes = [
 { path: '', component: HomePageComponent },
 { path: '', loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule) }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, {
        preloadingStrategy: PreloadAllModules
    })],
    exports: [RouterModule]
})
export class AppRoutingModule { }