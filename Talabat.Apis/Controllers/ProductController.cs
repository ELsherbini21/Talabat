using AutoMapper;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;
using Talabat.Apis.Dtos;
using Talabat.Apis.Errors;
using Talabat.Apis.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Product_Specs;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.Apis.Controllers
{


    public class ProductController : BaseApiController
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<PaginationResponse<Product>>> GetAll
        ([FromQuery] ProductSpecParams productSpecParams)
        {
            var products = await _service.GetAllAsync(productSpecParams);


            var productDTo = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturn_Dto>>(products);

            var productCountAfterSpec = await _service.GetCountAsync(productSpecParams);

            var pagination = new PaginationResponse<ProductToReturn_Dto>()
            {
                PageIndex = productSpecParams.PageIndex,
                PageSize = productSpecParams.PageSize,
                Data = productDTo,
                Count = productCountAfterSpec
            };

            return Ok(pagination);

        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);

            if (product is null)
                return NotFound(new ApiResponse(404));

            var productDTo = _mapper.Map<Product, ProductToReturn_Dto>(product);

            return Ok(productDTo);

        }


    }
}
