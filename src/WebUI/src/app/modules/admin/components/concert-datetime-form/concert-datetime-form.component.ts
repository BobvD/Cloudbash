import { Component, OnInit, Input } from '@angular/core';
import { Concert } from 'src/app/shared/models/concert.model';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { ConcertService } from 'src/app/shared/services/concert.service';

@Component({
    selector: 'app-concert-datetime-form',
    templateUrl: './concert-datetime-form.component.html',
    styleUrls: ['./concert-datetime-form.component.scss']
})
export class ConcertDatetimeFormComponent implements OnInit {
    @Input() concert = new Concert();
    minDate: Date;
    submitted = false;
   
    constructor(private concertService: ConcertService) { }

    ngOnInit(): void { }

    dateChange($event) {
        this.minDate = null;
                this.minDate = $event;
    }

    submit(): Observable<any> {
        this.submitted = !this.submitted;       
        return this.concertService.schedule(this.concert.Id, this.concert.StartDate, this.concert.EndDate);
    }
    
}
