import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-concert-grid',
    templateUrl: './concert-grid.component.html',
    styleUrls: ['./concert-grid.component.scss']
})
export class ConcertGridComponent implements OnInit {
    items = [1,2,3,4,5,6,7,8,9,10,11,12];

    constructor() { }

    ngOnInit(): void { 
        console.log('concert grid')
    }
}
