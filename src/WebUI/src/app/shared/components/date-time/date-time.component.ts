import { Component, OnInit, Input } from '@angular/core';
import { NgbDateStruct, NgbDate, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-date-time',
    templateUrl: './date-time.component.html',
    styleUrls: ['./date-time.component.scss']
})
export class DateTimeComponent implements OnInit {
    @Input() date: Date = new Date();   

    time = {hour: 20, minute: 30};
    model: NgbDateStruct;
    fromDate: NgbDate;
    
    constructor(private calendar: NgbCalendar) {
        this.fromDate = this.calendar.getToday();
        
      }

    ngOnInit(): void {
        this.oneTimeChange(this.time);
    }

    dateToNgbDate(date: Date){  
         
        const ngbDate =  new NgbDate(
            date.getFullYear(), 
            date.getMonth() + 1, 
            date.getDate());            
        return ngbDate;
        
    }

    onDateSelect($event){        
        this.date.setFullYear($event.year);
        this.date.setMonth($event.month - 1);
        this.date.setDate($event.day);
    }

    oneTimeChange($event){
        this.date.setHours($event.hour);
        this.date.setMinutes($event.minute);
    }
   
}
