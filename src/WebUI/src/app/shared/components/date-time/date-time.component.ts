import { Component, OnInit, Input, OnChanges, SimpleChanges, Output, EventEmitter, ChangeDetectorRef } from '@angular/core';
import { NgbDateStruct, NgbDate, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-date-time',
    templateUrl: './date-time.component.html',
    styleUrls: ['./date-time.component.scss']
})
export class DateTimeComponent implements OnInit, OnChanges {
    @Input() date: Date = new Date();
    @Input() 
    set minDate(date: Date) { 
        this.date = date;   
    
      this.fromDate = this.dateToNgbDate(date);
    }

    @Output() dateChange:EventEmitter<Date> = new EventEmitter<Date>();


    time = {hour: 20, minute: 30};

    model: NgbDateStruct;
    fromDate: NgbDate;
    
    constructor(private calendar: NgbCalendar, private cdRef:ChangeDetectorRef) {
        this.fromDate = this.calendar.getToday();
      }

    ngOnInit(): void {
        
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
        this.dateChange.emit(this.date);
    }

    oneTimeChange($event){
        this.date.setHours($event.hour);
        this.date.setMinutes($event.minute);
        this.dateChange.emit(this.date);
    }

    ngOnChanges(changes: SimpleChanges) {     
        console.log(changes)
        if(changes.minDate){
        console.log(changes.minDate)
           this.fromDate = this.dateToNgbDate(changes.minDate.currentValue);
        }    
      }
}
