
import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { HomePageComponent } from './shared/pages/home-page/home-page.component';
import { AuthLayoutComponent } from './shared/layouts/auth-layout/auth-layout.component';
import { AdminLayoutComponent } from './shared/layouts/admin-layout/admin-layout.component';
import { AuthGuard } from './shared/guards/auth.guard';
import { Role } from './shared/models/user.model';
import { MainLayoutComponent } from './shared/layouts/main-layout/main-layout.component';

const routes: Routes = [
    { path: '', component: HomePageComponent },   
    {
        path: '',
        component: MainLayoutComponent,
        children: [
            { path: 'cart', loadChildren: () => import('./modules/cart/cart.module').then(m => m.CartModule) },
        ]
    },
    {
        path: '',
        component: AuthLayoutComponent,
        children: [
            { path: '', loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule) }
        ]
    },
    {
        path: 'admin',
        component: AdminLayoutComponent,
        children: [
            { path: '', loadChildren: () => import('./modules/admin/admin.module').then(m => m.AdminModule) }
        ],
        canActivate: [
            AuthGuard
        ],
        data: { 
            roles: [ Role.Admin ] 
        }
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, {
        preloadingStrategy: PreloadAllModules
    })],
    exports: [RouterModule]
})
export class AppRoutingModule { }