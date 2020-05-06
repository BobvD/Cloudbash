import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from './config.service';

@Injectable({
    providedIn: 'root',
  })
export class ConcertService {

    private _concertUrl: string;

    constructor(private http: HttpClient,
                private configService: ConfigService) {
        this._concertUrl = this.configService.getApiBaseUrl() + '/concerts/';
    }

    get(): Observable<any> {
        return this.http.get<any>(this._concertUrl);
    }

    create(concert: any): Observable<any> {
        return this.http.post<any>(this._concertUrl, concert);
    }

    delete(id: any): Observable<any> {
        return this.http.delete<any>(this._concertUrl + id);
    }

}