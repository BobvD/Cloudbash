import { Venue } from './venue.model';
import { TicketType } from './ticket-type.model';

export class Concert {
    Id: string;
    Name: string;
    Venue: Venue;
    VenueId: string;
    ImageUrl: string;
    Date: string;
    TicketTypes: TicketType[] = [];
}