using StackExchange.Redis;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        // here i make connection with redis serveice , 
         // and i mkae connection at database that exist in redis . 
        private readonly IConnectionMultiplexer _redis;

        // we must have object from rediust connection . 
        public BasketRepository(IConnectionMultiplexer redis) // 
        {
            _redis= redis;
        }

        public Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket customerBasket)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBasketAsync(string basketId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerBasket> GetBasketAsyncById(string basketId)
        {
            throw new NotImplementedException();
        }
    }
}
