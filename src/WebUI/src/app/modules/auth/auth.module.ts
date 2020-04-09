import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthRoutingModule } from './auth-routing.module';
import { SignInPageComponent } from './pages/signin-page/signin-page.component';
import { AmplifyAngularModule } from 'aws-amplify-angular';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';

@NgModule({
    declarations: [
        SignInPageComponent
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