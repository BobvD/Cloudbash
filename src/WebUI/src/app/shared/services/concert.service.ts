import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root',
  })
export class ConcertService {
    private _concertUrl: string;

    constructor(private http: HttpClient) {
        this._concertUrl = 'https://03rdp1k0sf.execute-api.us-east-1.amazonaws.com/dev/concerts';
    }

    get(): Observable<any> {
        return this.http.get<any>(this._concertUrl);
    }

}