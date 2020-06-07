using Cloudbash.Application.Carts.Queries.GetCart;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ReadModels;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Carts.Queries.GetCart
{
    [Collection("QueryTests")]
    public class GetCartQueryTests
    {
        private readonly IViewModelRepository<Cart> _repo;

        public GetCartQueryTests(TestFixture fixture)
        {
            _repo = fixture.CartRepository;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVm()
        {
            var query = new GetCartQuery
            {
                CustomerId = new Guid("6fe6c9a1-2ee9-4413-9123-d840ca7ead49")
            };

            var handler = new GetCartQuery.GetCartQueryHandler(_repo);

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<Cart>();
            result.Items.Count.ShouldBe(2);
            result.Id.ShouldBe("e43b8063-23f2-4835-a35a-503ae5e09673");
        }
    }
}