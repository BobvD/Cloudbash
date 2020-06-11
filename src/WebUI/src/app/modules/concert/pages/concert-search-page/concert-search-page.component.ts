import { Component, OnInit } from '@angular/core';
import { ConcertService } from 'src/app/shared/services/concert.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-concert-search-page',
    templateUrl: './concert-search-page.component.html',
    styleUrls: ['./concert-search-page.component.scss']
})

export class ConcertSearchPageComponent implements OnInit {

    concerts = [];
    error = false;
    term = "";
    time: number;
    busy = false;

    constructor(private route: ActivatedRoute,
        private concertService: ConcertService) { }


    ngOnInit(): void {
        this.route.queryParams.subscribe(params => {

            this.term = params.q;
            this.time = +params.t;
            if (this.term) {
                this.busy = true;
                this.loadPage(1);
            }
        });
    }


    loadPage(page: number) {
        this.concertService.filter(this.term).subscribe(res => {
            this.busy = false;
            this.concerts = res.Concerts;
            if (res.Count < 1) {                
                this.error = true;
            }
        }, err => {
            this.busy = false;
            this.error = true;
        })
    }

    getTimeString(key: number) {
        switch (key) {
            case 0:
                return "Any Day"
                break;
            case 1:
                return "Today"
                break;
            case 2:
                return "This week"
                break;
            case 3:
                return "This weekend"
                break;
            case 4:
                return "Next week"
                break;
            default:
                break;
        }
    }
}
