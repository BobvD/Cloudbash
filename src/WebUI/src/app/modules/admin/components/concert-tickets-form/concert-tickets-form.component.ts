import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TicketTypeCreateModalComponent } from '../ticket-type-create-modal/ticket-type-create-modal.component';
import { Concert } from 'src/app/shared/models/concert.model';
import { ConcertService } from 'src/app/shared/services/concert.service';
import { TicketType } from 'src/app/shared/models/ticket-type.model';

@Component({
    selector: 'app-concert-tickets-form',
    templateUrl: './concert-tickets-form.component.html',
    styleUrls: ['./concert-tickets-form.component.scss']
})
export class ConcertTicketsFormComponent implements OnInit {
    
    @Output() deleteTicketType:EventEmitter<TicketType> = new EventEmitter<TicketType>();
    @Input() concert: Concert;
    
    busy = false;
    selected: TicketType = null;

    constructor(private modalService: NgbModal,
                private concertService: ConcertService) { }

    ngOnInit(): void { }

    openTicketTypeCreateModal(){
        const modalRef = this.modalService.open(TicketTypeCreateModalComponent, { centered: true });
        modalRef.componentInstance.concert = this.concert;
        modalRef.componentInstance.modalClose.subscribe(($e) => {
            this.concert.TicketTypes.push($e);
        });
    }

    removeTicketType(type: TicketType) {
        this.busy = true;
        this.selected = type;
        this.concertService.removeTicketType(type.Id, this.concert.Id).subscribe(res => {  
            this.deleteTicketType.emit(type);
            this.busy = false;
            this.selected = null;
        }, err => {
            this.busy = false;
            this.selected = null;
        })        
    }
}
