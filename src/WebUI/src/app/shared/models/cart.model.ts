import { TicketType } from './ticket-type.model';

export class Cart {
    Id: string;
    CustomerId: string;
    Items: CartItem[] = [];
}

export class CartItem {
    Id: string;
    Quantity: number;
    TicketTypeId: string;
    TicketType: TicketType;
}
