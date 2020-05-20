import { Venue } from './venue.model';
import { TicketType } from './ticket-type.model';

export class Concert {
    Id: string;
    Name: string;
    Venue: Venue;
    VenueId: string;
    ImageUrl: string;
    StartDate: Date = new Date();
    EndDate: Date = new Date();
    TicketTypes: TicketType[] = [];
}