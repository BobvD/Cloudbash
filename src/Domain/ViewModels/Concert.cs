using Cloudbash.Domain.Concerts;
using Cloudbash.Domain.SeedWork;

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

        public Concert() { }
    }
}
