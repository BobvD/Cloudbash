using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Application.Venues.Queries.GetVenues;
using Cloudbash.Domain.ReadModels;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace Cloudbash.Application.UnitTests.Venues.Queries.GetVenues
{
    [Collection("QueryTests")]
    public class GetVenuesQueryTests
    {
        private readonly IViewModelRepository<Venue> _repo;

        public GetVenuesQueryTests(TestFixture fixture)
        {
            _repo = fixture.VenueRepository;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVmAndListCount()
        {
            var query = new GetVenuesQuery();

            var handler = new GetVenuesQuery.GetVenuesQueryHandler(_repo);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<GetVenuesVm>();
            result.Venues.Count.ShouldBe(1);
            result.Count.ShouldBe(result.Venues.Count);
        }
    }
}