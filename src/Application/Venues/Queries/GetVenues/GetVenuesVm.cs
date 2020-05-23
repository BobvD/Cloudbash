using Cloudbash.Domain.ReadModels;
using System.Collections.Generic;

namespace Cloudbash.Application.Venues.Queries.GetVenues
{
    public class GetVenuesVm
    {
        public IList<Venue> Venues { get; set; }
        public int Count { get; set; }
    }
}
