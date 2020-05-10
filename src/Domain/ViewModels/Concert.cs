using Cloudbash.Domain.SeedWork;

namespace Cloudbash.Domain.ViewModels
{
    public class Concert : ReadModelBase
    {
        public string Name { get; set; }
        public string VenueId { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }

        public Concert() { }
    }
}
