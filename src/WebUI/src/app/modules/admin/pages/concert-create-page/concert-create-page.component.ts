import { Component, ViewChild } from '@angular/core';
import { Concert } from 'src/app/shared/models/concert.model';
import { ConcertDetailsFormComponent } from '../../components/concert-details-form/concert-details-form.component';
import { WizardComponent } from 'angular-archwizard';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-concert-create-page',
  templateUrl: './concert-create-page.component.html',
  styleUrls: ['./concert-create-page.component.scss']
})
export class ConcertCreatePageComponent {
  

  @ViewChild(ConcertDetailsFormComponent, null) details: ConcertDetailsFormComponent;
  @ViewChild(WizardComponent, null) public wizard: WizardComponent;
  
  concert = new Concert();
  date = new Date();
  busy = false;
  details_status: string;
  

  constructor(private toastr: ToastrService){}
  
  submit(){
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

}
