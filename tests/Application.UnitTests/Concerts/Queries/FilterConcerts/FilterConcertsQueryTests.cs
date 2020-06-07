using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Application.Concerts.Queries.FilterConcerts;
using Cloudbash.Domain.ReadModels;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Concerts.Queries.FilterConcerts
{
    [Collection("QueryTests")]
    public class FilterConcertsQueryTests
    {
        private readonly IViewModelRepository<Concert> _repo;

        public FilterConcertsQueryTests(TestFixture fixture)
        {
            _repo = fixture.ConcertRepository;
        }

        [Fact]
        public async Task When_Filter_By_Name_Handle_ReturnsCorrectVmAndListCount()
        {
            var query = new FilterConcertsQuery
            {
                SearchTerm = "Mumford",
                VenueName = ""
            };

            var handler = new FilterConcertsQuery.FilterConcertsQueryHandler(_repo);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<FilterConcertsVm>();
            result.Concerts.Count.ShouldBe(1);
        }

        [Fact]
        public async Task When_Filter_By_Venue_Handle_ReturnsCorrectVmAndListCount()
        {
            var query = new FilterConcertsQuery
            {
                SearchTerm = "",
                VenueName = "Ziggo Dome"
            };

            var handler = new FilterConcertsQuery.FilterConcertsQueryHandler(_repo);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<FilterConcertsVm>();
            result.Concerts.Count.ShouldBe(1);
        }

        [Fact]
        public async Task When_Filter_By_Invalid_Name_Handle_ReturnsCorrectVmAndListCount()
        {
            var query = new FilterConcertsQuery
            {
                SearchTerm = "Lady Gaga",
                VenueName = ""
            };

            var handler = new FilterConcertsQuery.FilterConcertsQueryHandler(_repo);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<FilterConcertsVm>();
            result.Concerts.Count.ShouldBe(0);
        }
    }
}