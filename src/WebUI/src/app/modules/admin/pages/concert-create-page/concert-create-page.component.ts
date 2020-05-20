import { Component, ViewChild } from '@angular/core';
import { Concert } from 'src/app/shared/models/concert.model';
import { ConcertDetailsFormComponent } from '../../components/concert-details-form/concert-details-form.component';
import { WizardComponent } from 'angular-archwizard';
import { ToastrService } from 'ngx-toastr';
import { TicketType } from 'src/app/shared/models/ticket-type.model';
import { ConcertDatetimeFormComponent } from '../../components/concert-datetime-form/concert-datetime-form.component';
import { ConcertService } from 'src/app/shared/services/concert.service';

@Component({
  selector: 'app-concert-create-page',
  templateUrl: './concert-create-page.component.html',
  styleUrls: ['./concert-create-page.component.scss']
})
export class ConcertCreatePageComponent {
  

  @ViewChild(ConcertDetailsFormComponent, null) details: ConcertDetailsFormComponent;
  @ViewChild(ConcertDatetimeFormComponent, null) dateTime: ConcertDatetimeFormComponent;
  @ViewChild(WizardComponent, null) public wizard: WizardComponent;
  
  concert = new Concert();
  date = new Date();
  busy = false;
  details_status: string;
  
  step_1 = "Info";
  step_2 = "Tickets";
  step_3 = "Date and Time";

  constructor(private toastr: ToastrService,
              private concertService: ConcertService){}

  submit(){
    
    // this.wizard.goToNextStep();    
    const currentStep = this.wizard.currentStep.stepTitle;

     if(this.concert.Id && currentStep == this.step_1) {
      this.wizard.goToNextStep();   
     }
     if(!this.concert.Id && currentStep == this.step_1) {
      this.createConcert();
     }
     if(currentStep == this.step_2) {
      this.wizard.goToNextStep(); 
     }
     if(currentStep == this.step_3) {
       this.scheduleConcert();
     }     
  }

  deleteTicketType(type: TicketType) {
    this.concert.TicketTypes = this.concert.TicketTypes.filter(t => t !== type);
  }

  createConcert() {
    this.busy = true;    
    this.details.submit().subscribe(res => {
      this.concert.Id = res;
      this.wizard.goToNextStep();
      this.busy = false;
    }, err => {
        this.toastr.error(`Could not create concert.`, 'Error');    
        this.busy = false;
    });     
  }

  scheduleConcert() {
    this.busy = true;   
    this.dateTime.submit().subscribe(res => {
      this.publish();
      console.log(res);
    }, err => {
      console.log(err);
    })
  }

  publish() {
    this.concertService.publish(this.concert.Id).subscribe(res => {
      console.log(res);
    }, err => {
      console.log(err);
    })
  }

}
