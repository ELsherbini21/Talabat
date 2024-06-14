using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Product_Specs;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync(ProductSpecParams productSpecParams)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(productSpecParams);

            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            if (products?.Count() > 0)
                return products;
            else
                return null;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec);

            if (product is not null)
                return product;
            else
                return null;
        }

        public async Task<int> GetCountAsync(ProductSpecParams productSpecParams)
        {
            var productCount_Spec = new ProductWithFiltrationForCount_Spec(productSpecParams);

            var productCountAfterSpec = await _unitOfWork.Repository<Product>().GetCountAsync(productCount_Spec);

            return productCountAfterSpec;
        }
    }
}
