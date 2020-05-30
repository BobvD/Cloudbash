import { Component, OnInit, Input } from '@angular/core';
import { Concert } from 'src/app/shared/models/concert.model';

@Component({
    selector: 'app-concert-grid',
    templateUrl: './concert-grid.component.html',
    styleUrls: ['./concert-grid.component.scss']
})
export class ConcertGridComponent implements OnInit {
    @Input() concerts: Concert[] = [];
 
    constructor() { }

    ngOnInit(): void { 
    }
}
