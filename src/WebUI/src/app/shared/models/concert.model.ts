import { Venue } from './venue.model';
import { TicketType } from './ticket-type.model';

export class Concert {
    Id: string;
    Name: string;
    Venue: Venue;
    VenueId: string;
    ImageUrl: string;
    Status: ConcertStatus;
    StartDate: Date = new Date();
    EndDate: Date = new Date();
    TicketTypes: TicketType[] = [];
}

export enum ConcertStatus {    
    DRAFT = 0,
    PUBLISHED = 1,
    DELETED = 2
}