import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Concert } from 'src/app/shared/models/concert.model';
import { ConcertService } from 'src/app/shared/services/concert.service';

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

    constructor(private formBuilder: FormBuilder,
                private concertService: ConcertService) { }

    ngOnInit(): void {
        this.concertForm = this.createFormGroup();
     }

    selectImage(image) {
        this.selectedImage = image;
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
          this.setValues();
          console.log(this.concert);

          this.concertService.create(this.concert).subscribe(res => {
              console.log(res);
          });
      }


      setValues() {
        this.concert.Name = this.concertForm.controls['name'].value;
        this.concert.Venue = this.concertForm.controls['venue'].value;
        this.concert.Date = this.concertForm.controls['date'].value;
        this.concert.ImageUrl = this.selectedImage;
      }

}
