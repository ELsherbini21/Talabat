using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Apis.Controllers
{
    public class ProductBrandController : BaseApiController
    {
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IMapper _mapper;

        public ProductBrandController(IGenericRepository<ProductBrand> brandRepo, IMapper mapper)
        {
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductBrandToReturn_Dto>>> GetAll()
        {
            // becasue there is no Navigation Property
            var brands = await _brandRepo.GetAllAsync();

            if (brands.Count() == 0)
                return NotFound();

            var brands_Dto = _mapper.Map<IReadOnlyList<ProductBrand>, IReadOnlyList<ProductBrandToReturn_Dto>>(brands);

            return Ok(brands_Dto);

        }
    }
}
