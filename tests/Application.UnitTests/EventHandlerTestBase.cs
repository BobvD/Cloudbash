using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ReadModels;
using System;
using System.Threading.Tasks;

namespace Cloudbash.Application.UnitTests
{
    public abstract class EventHandlerTestBase
    {

        protected readonly IViewModelRepository<Concert> _concertRepo;
        protected readonly IViewModelRepository<Venue> _venueRepo;
        protected readonly IViewModelRepository<Cart> _cartRepo;

        protected readonly TestFixture _fixture;

        public EventHandlerTestBase(TestFixture fixture)
        {
            _fixture = fixture;
            _concertRepo = fixture.ConcertRepository;
            _venueRepo = fixture.VenueRepository;
            _cartRepo = fixture.CartRepository;
        }

        protected async Task<Domain.Concerts.Concert> CreateAndSaveNewConcertAggregate()
        {
            var concert = new Domain.Concerts.Concert(
                    null,
                    new Guid("2bfb53d2-549d-438b-a8be-e87a7457abd7"),
                    null
                    );

            var concertVm = new Concert
            {
                Id = concert.Id.ToString()
            };

            await _concertRepo.AddAsync(concertVm);

            return concert;
        }

        protected async Task<Domain.Carts.Cart> CreateAndSaveCartAggregate()
        {
            var cart = new Domain.Carts.Cart(Guid.NewGuid());

            var cartVm = new Cart
            {
                Id = cart.Id.ToString(),
                CustomerId = cart.CustomerId
            };

            await _cartRepo.AddAsync(cartVm);

            return cart;
        }

    }
}
