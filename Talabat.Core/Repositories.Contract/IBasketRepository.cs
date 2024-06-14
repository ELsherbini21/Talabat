using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositories.Contract
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsyncById(string basketId);

        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket customerBasket); // create or up

        Task<bool> DeleteBasketAsync(string basketId);
    }
}
