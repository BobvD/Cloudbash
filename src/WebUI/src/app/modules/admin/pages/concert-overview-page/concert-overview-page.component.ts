import { Component, OnInit, ViewChild } from '@angular/core';
import { ConcertService } from 'src/app/shared/services/concert.service';
import { DatatableComponent, ColumnMode, SelectionType } from '@swimlane/ngx-datatable';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-concert-overview-page',
    templateUrl: './concert-overview-page.component.html',
    styleUrls: ['./concert-overview-page.component.scss']
})
export class ConcertOverviewPageComponent implements OnInit {
    rows = [];

  loadingIndicator = true;
  reorderable = true;
  selected = [];
  temp = [];

  ColumnMode = ColumnMode;
  SelectionType = SelectionType;

  @ViewChild(DatatableComponent, { static: false }) table: DatatableComponent;

    constructor(private concertService: ConcertService,
        private toastr: ToastrService) { }

    ngOnInit(): void {
        this.concertService.get().subscribe(res => {
            this.temp = [...res.Concerts];
            this.rows = res.Concerts;
        })
     }

      onSelect({ selected }) {
    this.selected.splice(0, this.selected.length);
    this.selected.push(...selected);
  }

  onActivate(event) {
  }

  updateFilter(event) {
    const val = event.target.value.toLowerCase();
    const temp = this.temp.filter(function (d) {
      return d.name.toLowerCase().indexOf(val) !== -1 || !val;
    });
    this.rows = temp;
    this.table.offset = 0;
  }

  removeSelected() {
      
    this.selected.forEach(concert => {
      this.concertService.delete(concert.Id).subscribe(res => {
        this.toastr.success(`'${concert.Id}' has been deleted.`, 'The concert had been deleted');
        this.rows = this.rows.filter(function (obj) {
          return obj.Id !== concert.Id;
        });
      }, err => {
        this.toastr.error(`Could not delete concert: '${concert.Id}'`, 'Error');
      });
    });
    this.selected = [];
    
  }

}
