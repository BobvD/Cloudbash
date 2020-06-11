using Cloudbash.Application.Common.Exceptions;
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
                SearchTerm = "Mumford"
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
                SearchTerm = "Ziggo Dome"
            };

            var handler = new FilterConcertsQuery.FilterConcertsQueryHandler(_repo);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<FilterConcertsVm>();
            result.Concerts.Count.ShouldBe(1);
            result.Count.ShouldBe(result.Concerts.Count);
        }

        [Fact]
        public async Task When_Filter_By_Venue_Handle_And_BeforeDate_ReturnsCorrectVmAndListCount()
        {
            var query = new FilterConcertsQuery
            {
                SearchTerm = "Ziggo Dome",
                Before = new System.DateTime(2020, 12, 11)
            };

            var handler = new FilterConcertsQuery.FilterConcertsQueryHandler(_repo);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<FilterConcertsVm>();
            result.Concerts.Count.ShouldBe(1);
            result.Count.ShouldBe(result.Concerts.Count);
        }


        [Fact]
        public async Task When_Filter_By_Venue_Handle_And_Invalid_BeforeDate_ReturnsCorrectVmAndListCount()
        {
            var query = new FilterConcertsQuery
            {
                SearchTerm = "Ziggo Dome",
                Before = new System.DateTime(2020, 12, 9)
            };

            var handler = new FilterConcertsQuery.FilterConcertsQueryHandler(_repo);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<FilterConcertsVm>();
            result.Concerts.Count.ShouldBe(0);
            result.Count.ShouldBe(result.Concerts.Count);
        }

        [Fact]
        public async Task When_Filter_By_Venue_Handle_And_AfterDate_ReturnsCorrectVmAndListCount()
        {
            var query = new FilterConcertsQuery
            {
                SearchTerm = "Ziggo Dome",
                After = new System.DateTime(2020, 12,9)
            };

            var handler = new FilterConcertsQuery.FilterConcertsQueryHandler(_repo);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<FilterConcertsVm>();
            result.Concerts.Count.ShouldBe(1);
            result.Count.ShouldBe(result.Concerts.Count);
        }


        [Fact]
        public async Task When_Filter_By_Venue_Handle_And_Invalid_AfterDate_ReturnsCorrectVmAndListCount()
        {
            var query = new FilterConcertsQuery
            {
                SearchTerm = "Ziggo Dome",
                After = new System.DateTime(2020, 12, 12)
            };

            var handler = new FilterConcertsQuery.FilterConcertsQueryHandler(_repo);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<FilterConcertsVm>();
            result.Concerts.Count.ShouldBe(0);
            result.Count.ShouldBe(result.Concerts.Count);
        }

        [Fact]
        public async Task When_Filter_By_Invalid_Name_Handle_ReturnsCorrectVmAndListCount()
        {
            var query = new FilterConcertsQuery
            {
                SearchTerm = "Lady Gaga"
            };

            var handler = new FilterConcertsQuery.FilterConcertsQueryHandler(_repo);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<FilterConcertsVm>();
            result.Concerts.Count.ShouldBe(0);
        }

        [Fact]
        public async Task When_Filter_By_Venue_Handle_And_Invalid_BeforeDate_ThrowsError()
        {
            var query = new FilterConcertsQuery
            {
                SearchTerm = "Ziggo Dome",
                After = new System.DateTime(2020, 12, 12),
                Before = new System.DateTime(2020, 12, 10)
            };

            var handler = new FilterConcertsQuery.FilterConcertsQueryHandler(_repo);

            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(query, CancellationToken.None));
        }
    }
}