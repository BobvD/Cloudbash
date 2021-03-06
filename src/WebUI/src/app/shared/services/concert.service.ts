import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from './config.service';
import { TicketType } from '../models/ticket-type.model';

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

    getById(id: string): Observable<any> {
        const url = `${this._concertUrl}${id}`
        return this.http.get<any>(url);
    }

    filter(term:string): Observable<any> {
        const url = `${this._concertUrl}filter?searchTerm=${term}`
        return this.http.get<any>(url);
    }

    create(concert: any): Observable<any> {
        return this.http.post<any>(this._concertUrl, concert);
    }

    addTicketType(ticketType: TicketType, concertId: string): Observable<any> {
        const url = `${this._concertUrl}${concertId}/ticket_type`
        return this.http.post<any>(url, ticketType);
    }

    removeTicketType(ticketId: string, concertId: string): Observable<any> {
        const url = `${this._concertUrl}${concertId}/ticket_type/${ticketId}`;
        return this.http.delete<any>(url);
    }

    schedule(concertId: string, startDate: Date, endDate: Date) {
        const url = `${this._concertUrl}${concertId}/schedule`
        return this.http.post<any>(url, { StartDate: startDate, EndDate: endDate });
    }

    publish(concertId: string) {
        const url = `${this._concertUrl}${concertId}/publish`
        return this.http.post<any>(url, {});
    }

    delete(id: any): Observable<any> {
        return this.http.delete<any>(this._concertUrl + id);
    }

}