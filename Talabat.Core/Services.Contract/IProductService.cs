using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.Core.Services.Contract
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetAllAsync(ProductSpecParams productSpecParams);

        Task<Product?> GetByIdAsync(int id);

        Task<int> GetCountAsync(ProductSpecParams productSpecParams);
    }
}
