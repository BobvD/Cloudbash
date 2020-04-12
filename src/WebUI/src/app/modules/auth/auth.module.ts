import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthRoutingModule } from './auth-routing.module';
import { AmplifyAngularModule } from 'aws-amplify-angular';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { AuthPageComponent } from './pages/auth-page/auth-page.component';

@NgModule({
    declarations: [
        AuthPageComponent
    ],
    imports: [ 
        CommonModule,
        AuthRoutingModule,
        AmplifyAngularModule
     ],
    exports: [],
    providers: [
        AuthenticationService        
    ],
})
export class AuthModule {}