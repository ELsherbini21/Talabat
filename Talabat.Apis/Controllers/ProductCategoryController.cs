using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Apis.Controllers
{
    public class ProductCategoryController : BaseApiController
    {
        private readonly IGenericRepository<ProductCategory> _categoryRepo;
        private readonly IMapper _mapper;

        public ProductCategoryController(IGenericRepository<ProductCategory> categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductCategoryToReturn_Dto>>> GetAll()
        {
            // becasue there is no Navigation Property
            var categories = await _categoryRepo.GetAllAsync();

            var brands_Dto =
                _mapper.Map<IReadOnlyList<ProductCategory>, IReadOnlyList<ProductCategoryToReturn_Dto>>(categories);

            return Ok(brands_Dto);

        }
    }
}
