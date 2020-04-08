import { Component, OnInit } from '@angular/core';
import { ConcertService } from '../../services/concert.service';

@Component({
    selector: 'app-home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {
    concerts = [];
    constructor(private concertService: ConcertService) { }

    ngOnInit(): void { 
        this.concertService.get().subscribe(res => {
            this.concerts = res.Concerts;
        })
    }
}
