using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Application.Concerts.Queries.GetConcerts;
using Cloudbash.Domain.ReadModels;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Concerts.Queries.GetConcerts
{
    [Collection("QueryTests")]
    public class GetConcertsQueryTests
    {
        private readonly IViewModelRepository<Concert> _repo;

        public GetConcertsQueryTests(TestFixture fixture)
        {
            _repo = fixture.ConcertRepository;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVmAndListCount()
        {
            var query = new GetConcertsQuery();

            var handler = new GetConcertsQuery.GetConcertsQueryHandler(_repo);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<GetConcertsVm>();
            result.Concerts.Count.ShouldBe(1);

        }
    }
}