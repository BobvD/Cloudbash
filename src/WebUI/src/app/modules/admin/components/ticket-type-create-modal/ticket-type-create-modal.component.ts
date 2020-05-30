import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { TicketType } from 'src/app/shared/models/ticket-type.model';
import { ConcertService } from 'src/app/shared/services/concert.service';
import { Concert } from 'src/app/shared/models/concert.model';

@Component({
    selector: 'app-ticket-type-create-modal',
    templateUrl: './ticket-type-create-modal.component.html',
    styleUrls: ['./ticket-type-create-modal.component.scss']
})
export class TicketTypeCreateModalComponent implements OnInit {
    
    @Output() modalClose: EventEmitter<any> = new EventEmitter<any>();
    
    @Input() concert: Concert;
    
    submitted = false;
    error = false;
    busy = false;

    ticketTypeForm: FormGroup;
    name: FormControl;
    price: FormControl;
    quantity: FormControl;

    ticketType = new TicketType();

    constructor(private formBuilder: FormBuilder,
                public activeModal: NgbActiveModal,
                private concertService: ConcertService) { }

    ngOnInit(): void { 
        this.ticketTypeForm = this.createFormGroup();
    }

    createFormGroup() {
        this.name = new FormControl('', [
            Validators.required
        ]);
        this.price = new FormControl('', [
            Validators.required
        ]);
        this.quantity = new FormControl('', [
            Validators.required
        ]);       
        return this.formBuilder.group({
          name: this.name,
          price: this.price,
          quantity: this.quantity
        });
    }


    submit(){
        this.error = false;
        this.submitted = true;
        this.busy = true;
        this.setValues();
        
        if(this.ticketTypeForm.valid) {        
            this.concertService.addTicketType(this.ticketType, this.concert.Id).subscribe(res => {                            
                this.ticketType.Id = res;
                this.modalClose.next(this.ticketType);
                this.activeModal.close();   
            }, err => {
                this.error = true;
                this.submitted = false;
                this.busy = false;
            });
        }   
        
    }

    setValues() {
        this.ticketType.Name = this.name.value;
        this.ticketType.Price = +this.price.value;
        this.ticketType.Quantity = +this.quantity.value;
    }

}
