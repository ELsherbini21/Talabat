using System.Runtime.CompilerServices;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contract;

namespace Talabat.Service
{

    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IReadOnlyList<ProductCategory>> GetAllAsync()
        {
            // becasue there is no Navigation Property
            var categories = await _unitOfWork.Repository<ProductCategory>().GetAllAsync();

            return (categories?.Count() > 0) ? categories : null;
        }
    }
}
