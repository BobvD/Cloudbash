using AutoMapper;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Application.Concerts.Queries.GetConcert;
using Cloudbash.Domain.ReadModels;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Concerts.Queries.GetConcertDetail
{
    [Collection("QueryTests")]
    public class GetConcertDetailQueryTests
    {
        private readonly IViewModelRepository<Concert> _repo;
        private readonly IMapper _mapper;

        public GetConcertDetailQueryTests(TestFixture fixture)
        {
            _repo = fixture.ConcertRepository;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVmAndItem()
        {
            var query = new GetConcertDetailQuery { Id = new Guid("2bfb53d2-549d-438b-a8be-e87a7457abd7") };

            var handler = new GetConcertDetailQuery.GetConcertQueryHandler(_repo);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<Concert>();
            result.Id.ShouldBe("2bfb53d2-549d-438b-a8be-e87a7457abd7");
            result.Name.ShouldBe("Mumford & Sons");
            result.ImageUrl.ShouldBe("IMAGE_URL");
            result.StartDate.ShouldBe(new DateTime(2020, 12, 10, 8, 30, 00));
            result.EndDate.ShouldBe(new DateTime(2020, 12, 10, 10, 30, 00));
            result.Status.ShouldBe(Domain.Concerts.ConcertStatus.PUBLISHED);
            result.Created.ShouldBe(new DateTime(2020, 2, 20));

            result.Venue.Id.ShouldBe("5b5eb886-06c6-4aed-bb24-35a1373edf97");
            result.Venue.Name.ShouldBe("Ziggo Dome");
            result.Venue.Capacity.ShouldBe(20000);
            result.Venue.Description.ShouldBe("Music Hall");
            result.Venue.WebUrl.ShouldBe("WEB_URL");
            result.Venue.Address.ShouldBe("Amsterdam");

            result.TicketTypes.Count.ShouldBe(2);
                       
        }
    }
}