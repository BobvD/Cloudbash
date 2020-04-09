import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

// 3rd Party
import { AmplifyAngularModule, AmplifyService, AmplifyModules } from 'aws-amplify-angular';
import Auth from '@aws-amplify/auth';

import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { ConcertModule } from './modules/concert/concert.module';
import { AppRoutingModule } from './app-routing.module';
import { HomePageComponent } from './shared/pages/home-page/home-page.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent
  ],
  imports: [
    BrowserModule,
    ConcertModule,
    SharedModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: AmplifyService,
      useFactory:  () => {
        return AmplifyModules({
          Auth
        });
      }
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
