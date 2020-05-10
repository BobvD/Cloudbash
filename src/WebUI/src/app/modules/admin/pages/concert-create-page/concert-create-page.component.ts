import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Concert } from 'src/app/shared/models/concert.model';
import { ConcertService } from 'src/app/shared/services/concert.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { FileService } from 'src/app/shared/services/file.service';
import { Observable } from 'rxjs';
import { Venue } from 'src/app/shared/models/venue.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { VenueCreateModalComponent } from '../../components/venue-create-modal/venue-create-modal.component';
import { VenueService } from 'src/app/shared/services/venue.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-concert-create-page',
  templateUrl: './concert-create-page.component.html',
  styleUrls: ['./concert-create-page.component.scss']
})
export class ConcertCreatePageComponent implements OnInit {

  images = ['https://cloudbash-frontend.s3.amazonaws.com/concer_placehold_1.jpg',
            'https://cloudbash-frontend.s3.amazonaws.com/concer_placehold_2.jpg',
            'https://cloudbash-frontend.s3.amazonaws.com/concer_placehold_3.jpg',
            'https://cloudbash-frontend.s3.amazonaws.com/concer_placehold_4.jpg',
            'https://cloudbash-frontend.s3.amazonaws.com/concer_placehold_5.jpg',
            'https://cloudbash-frontend.s3.amazonaws.com/concer_placehold_6.jpg',
            'https://cloudbash-frontend.s3.amazonaws.com/concer_placehold_7.jpg',
            'https://cloudbash-frontend.s3.amazonaws.com/concer_placehold_8.jpg',
            'https://cloudbash-frontend.s3.amazonaws.com/concer_placehold_9.jpg'];

  selectedImage = null;
  date = new Date();
  concertForm: FormGroup;
  concert = new Concert();

  venues$: Observable<Venue[]>;

  constructor(private formBuilder: FormBuilder,
    private concertService: ConcertService,
    private venueService: VenueService,
    private toastr: ToastrService,
    private modalService: NgbModal,
    private router: Router) { }

  ngOnInit(): void {
    this.concertForm = this.createFormGroup();
    this.loadVenues();
  }

  selectImage(image) {
    this.selectedImage = image;
    if (!this.images.includes(image))
      this.images.unshift(image);
  }

  createFormGroup() {
    return this.formBuilder.group({
      name: '',
      description: '',
      date: '',
      venue: '',
    });
  }

  submit() {
    console.log(this.concert)
    this.setValues();
    this.concertService.create(this.concert).subscribe(res => {
      this.toastr.success(`The concert has been created with id: ` + res, 'Success!');
      this.router.navigate(['/admin/concerts']);
    }, err => {
      this.toastr.error(`Could not create concert.`, 'Error');
    });
  }

  setValues() {
    this.concert.Name = this.concertForm.controls['name'].value;
    this.concert.Venue = this.concertForm.controls['venue'].value;
    this.concert.Date = this.concertForm.controls['date'].value;
    this.concert.ImageUrl = this.selectedImage;
  }


  openVenueCreateModal() {
    const modalRef = this.modalService.open(VenueCreateModalComponent, { centered: true });   
    modalRef.componentInstance.modalClose.subscribe(($e) => {
      console.log($e);
      this.loadVenues();
      this.concert.VenueId = $e.id;

      

    })
  }

  loadVenues() {
    this.venues$ = null;
    this.venues$ = this.venueService.get().pipe(
      map(x => x.Venues)
    );
  }

}
