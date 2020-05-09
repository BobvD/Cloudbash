import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { VenueService } from 'src/app/shared/services/venue.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Venue } from 'src/app/shared/models/venue.model';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-venue-create-modal',
    templateUrl: './venue-create-modal.component.html',
    styleUrls: ['./venue-create-modal.component.scss']
})
export class VenueCreateModalComponent implements OnInit {

    submitted = false;
    error = false;

    venueForm: FormGroup;
    name: FormControl;
    description: FormControl;
    capacity: FormControl;
    weburl: FormControl;
    address: FormControl;

    venue =  new Venue();
    
    constructor(private formBuilder: FormBuilder,
                public activeModal: NgbActiveModal,
                public venueService: VenueService,
                private toastr: ToastrService) { }

    ngOnInit(): void { 
        this.venueForm = this.createFormGroup();
    }

    createFormGroup() {
        this.name = new FormControl('', [
            Validators.required
        ]);
        this.description = new FormControl('');
        this.capacity = new FormControl('');
        this.weburl = new FormControl('');
        this.address = new FormControl('', [
            Validators.required
        ]);

        return this.formBuilder.group({
          name: this.name,
          description: this.description,
          capacity: this.capacity,
          weburl: this.weburl,
          address: this.address
        });
    }

    submit(){
        this.error = false;
        this.submitted = true;
        this.setValues();
        if(this.venueForm.valid) {        
            this.venueService.create(this.venue).subscribe(res => {
                this.toastr.success(`'${this.venue.Name}' has been created with id: ${res}`, 'The Venue had been added');
                this.activeModal.close();
            }, err => {
                this.error = true;
                console.log(err);
            });
        }        
    }

    setValues() {
        this.venue.Name = this.name.value;
        this.venue.Description = this.description.value;
        this.venue.Capacity = +this.capacity.value;
        this.venue.WebUrl = this.weburl.value;
        this.venue.Address = this.address.value;
      }
}