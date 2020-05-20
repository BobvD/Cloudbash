import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Concert } from 'src/app/shared/models/concert.model';
import { ConcertService } from 'src/app/shared/services/concert.service';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Venue } from 'src/app/shared/models/venue.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { VenueCreateModalComponent } from '../../components/venue-create-modal/venue-create-modal.component';
import { VenueService } from 'src/app/shared/services/venue.service';
import { map } from 'rxjs/operators';

@Component({
    selector: 'app-concert-details-form',
    templateUrl: './concert-details-form.component.html',
    styleUrls: ['./concert-details-form.component.scss']
})
export class ConcertDetailsFormComponent implements OnInit {

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
    concertForm: FormGroup;
    @Input() concert = new Concert();
    submitted = false;
    venues$: Observable<Venue[]>;
    @Output() status: EventEmitter<string> = new EventEmitter();


    constructor(private formBuilder: FormBuilder,
        private concertService: ConcertService,
        private venueService: VenueService,
        private modalService: NgbModal,
        private router: Router) { }

    ngOnInit(): void {
        this.concertForm = this.createFormGroup();
        this.loadVenues();


        this.concertForm.valueChanges.subscribe(res => {
            console.log(this.concertForm.status);
            this.status.next(this.concertForm.status);
        });
    }

    selectImage(image) {
        this.selectedImage = image;
        if (!this.images.includes(image))
            this.images.unshift(image);
    }

    name: FormControl;
    description: FormControl;
    venue: FormControl;

    createFormGroup() {
        this.name = new FormControl('', [
            Validators.required
        ]);

        this.description = new FormControl();
        this.venue = new FormControl();

        return this.formBuilder.group({
            name: this.name,
            description: this.description,
            venue: this.venue,
        });
    }

    submit(): Observable<any> {
        this.submitted = !this.submitted;
        this.setValues();
        return this.concertService.create(this.concert);
    }

    setValues() {
        this.concert.Name = this.concertForm.controls['name'].value;
        this.concert.Venue = this.concertForm.controls['venue'].value;
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
