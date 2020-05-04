import { Component, OnInit } from '@angular/core';
import { ConcertService } from '../../services/concert.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
    selector: 'app-home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {
    concerts = [];
    error = false;

    constructor(private concertService: ConcertService,
                private spinner: NgxSpinnerService) { }

    ngOnInit(): void { 
        this.spinner.show();
        this.concertService.get().subscribe(res => {
            this.spinner.hide();
            this.concerts = res.Concerts;
        }, err => {
            this.error = true;
            this.spinner.hide();
        })
    }
}
