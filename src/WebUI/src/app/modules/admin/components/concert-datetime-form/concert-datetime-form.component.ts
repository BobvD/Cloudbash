import { Component, OnInit, Input } from '@angular/core';
import { Concert } from 'src/app/shared/models/concert.model';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-concert-datetime-form',
    templateUrl: './concert-datetime-form.component.html',
    styleUrls: ['./concert-datetime-form.component.scss']
})
export class ConcertDatetimeFormComponent implements OnInit {
    @Input() concert = new Concert();
    minDate: Date;
   
    constructor() { }

    ngOnInit(): void { }

    dateChange($event) {
        console.log($event);
        this.minDate = null;
        
        this.minDate = $event;
    }
}
