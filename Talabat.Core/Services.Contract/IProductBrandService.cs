using Talabat.Core.Entities;

namespace Talabat.Core.Services.Contract
{
    public interface IProductBrandService
    {
        Task<IReadOnlyList<ProductBrand>> GetAllAsync();

    }



}
