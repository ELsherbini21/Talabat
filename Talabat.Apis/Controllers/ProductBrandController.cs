using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
using Talabat.Apis.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;

namespace Talabat.Apis.Controllers
{
    public class ProductBrandController : BaseApiController
    {
        private readonly IProductBrandService _service;
        private readonly IMapper _mapper;

        public ProductBrandController(IProductBrandService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductBrandToReturn_Dto>>> GetAll()
        {
            // becasue there is no Navigation Property
            var brands = await _service.GetAllAsync();

            var brands_Dto = _mapper.Map<IReadOnlyList<ProductBrand>, IReadOnlyList<ProductBrandToReturn_Dto>>(brands);

            return Ok(brands_Dto);

        }
    }
}
