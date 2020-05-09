import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from './config.service';

@Injectable({
    providedIn: 'root',
  })
export class VenueService {

    private _venueUrl: string;

    constructor(private http: HttpClient,
                private configService: ConfigService) {
        this._venueUrl = this.configService.getApiBaseUrl() + '/venues/';
    }

    get(): Observable<any> {
        return this.http.get<any>(this._venueUrl);
    }

    create(venue: any): Observable<any> {
        return this.http.post<any>(this._venueUrl, venue);
    }

    delete(id: any): Observable<any> {
        return this.http.delete<any>(this._venueUrl + id);
    }

}