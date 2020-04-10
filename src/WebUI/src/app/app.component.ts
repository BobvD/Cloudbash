import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { InfoBoxComponent } from './shared/components/info-box/info-box.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'cloudbash';

  constructor(private toastr: ToastrService){

  }
  ngOnInit(): void {
    this.toastr.success('Hello world!', 'Toastr fun!', { toastComponent: InfoBoxComponent });
    
  }
}
