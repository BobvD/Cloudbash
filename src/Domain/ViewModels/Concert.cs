using Cloudbash.Domain.Concerts;
using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Cloudbash.Domain.ViewModels
{
    public class Concert : ReadModelBase
    {
        public string Name { get; set; }        
        public string VenueId { get; set; }
        public virtual Venue Venue { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }
        public ConcertStatus Status { get; set; }
        public DateTime Created { get; set; }
        public List<TicketType> TicketTypes { get; set; }
        public Concert()
        {
            TicketTypes = new List<TicketType>();
        }
    }
}
