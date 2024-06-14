using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contract;

namespace Talabat.Service
{
    public class ProductBrandService : IProductBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductBrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetAllAsync()
        {
            // becasue there is no Navigation Property
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

            return (brands?.Count() > 0) ? brands : null;
        }
    }
}
