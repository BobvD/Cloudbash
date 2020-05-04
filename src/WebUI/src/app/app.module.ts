import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// 3rd Party
import { AmplifyService, AmplifyModules } from 'aws-amplify-angular';
import Auth from '@aws-amplify/auth'; 
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { NgxSpinnerModule } from "ngx-spinner";

import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { ConcertModule } from './modules/concert/concert.module';
import { AppRoutingModule } from './app-routing.module';
import { HomePageComponent } from './shared/pages/home-page/home-page.component';
import { HttpClientModule } from '@angular/common/http';
import { InfoBoxComponent } from './shared/components/info-box/info-box.component';

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
    HttpClientModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot({
      disableTimeOut: true,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      tapToDismiss: false
    }),
    NgxSpinnerModule
  ],
  providers: [
    {
      provide: AmplifyService,
      useFactory:  () => {
        return AmplifyModules({
          Auth
        });
      }
    },
    ToastrService
  ],
  bootstrap: [
    AppComponent
  ],
  entryComponents:  [
    InfoBoxComponent
  ]
})
export class AppModule { }
