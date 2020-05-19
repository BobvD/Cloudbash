import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TicketTypeCreateModalComponent } from '../ticket-type-create-modal/ticket-type-create-modal.component';
import { Concert } from 'src/app/shared/models/concert.model';

@Component({
    selector: 'app-concert-tickets-form',
    templateUrl: './concert-tickets-form.component.html',
    styleUrls: ['./concert-tickets-form.component.scss']
})
export class ConcertTicketsFormComponent implements OnInit {
    
    @Input() concert: Concert;
    
    constructor(private modalService: NgbModal) { }

    ngOnInit(): void { }


    openTicketTypeCreateModal(){
        const modalRef = this.modalService.open(TicketTypeCreateModalComponent, { centered: true });
        this.concert.Id = "04621d47-0507-4c37-9b4a-a29b00a2d09a";
        modalRef.componentInstance.concert = this.concert;
        modalRef.componentInstance.modalClose.subscribe(($e) => {
            console.log($e);
            this.concert.TicketTypes.push($e);
        });
    }
}
