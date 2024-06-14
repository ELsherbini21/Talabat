using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        // here i make connection with redis serveice , 
        // and i mkae connection at database that exist in redis . 
        private readonly IDatabase _redisDb; // to get data 

        // we must have object from rediust connection . 
        public BasketRepository(IConnectionMultiplexer redis) // 
        {
            _redisDb = redis.GetDatabase(); // it's from type IDataBase .
        }

        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket customerBasket)
        {
            // this method used for update and create 
            var createdOrUpdatedChecking = await _redisDb.StringSetAsync(customerBasket.Id,
                  JsonSerializer.Serialize(customerBasket),  // convert to json file .
                  TimeSpan.FromDays(30));

            // if isn't null , i will return the object that have been created or updated . 
            return (createdOrUpdatedChecking is false) ? null : await GetBasketAsyncById(customerBasket.Id);

        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _redisDb.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsyncById(string basketId)
        {
            var basket = await _redisDb.StringGetAsync(basketId); // baskest is struct

            return ((basket.IsNullOrEmpty) ? null : JsonSerializer.Deserialize<CustomerBasket>(basket));
        }
    }
}
