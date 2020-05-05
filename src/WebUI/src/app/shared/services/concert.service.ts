import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root',
  })
export class ConcertService {

    private _concertUrl: string;

    constructor(private http: HttpClient) {
        this._concertUrl = localStorage.getItem('apiUrl') + '/concerts/';
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