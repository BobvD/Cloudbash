using Cloudbash.Domain.SeedWork;

namespace Cloudbash.Domain.ViewModels
{
    public class Concert : ReadModelBase
    {
        public string Name { get; set; }
        public string Venue { get; set; }
        public string ImageUrl { get; set; }
        public string Date { get; set; }
    }
}
