using Talabat.Core.Entities;

namespace Talabat.Core.Services.Contract
{
    public interface IProductCategoryService
    {
        Task<IReadOnlyList<ProductCategory>> GetAllAsync();

    }



}
