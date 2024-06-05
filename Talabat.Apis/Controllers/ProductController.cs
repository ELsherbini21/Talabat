using AutoMapper;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;
using Talabat.Apis.Dtos;
using Talabat.Apis.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Product_Specs;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.Apis.Controllers
{


    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        //private readonly ISpecifications<Product> _specifications;

        public ProductController(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            //_specifications = specifications;
        }

        // baseurl / api / Product
        [HttpGet]
        public async Task<ActionResult<PaginationResponse<Product>>>
            GetAll([FromQuery] ProductSpecParams productSpecParams)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(productSpecParams);

            var products = await _productRepository.GetAllWithSpecAsync(spec);

            var productDTo = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturn_Dto>>(products);

            var productCount_Spec = new ProductWithFiltrationForCount_Spec(productSpecParams);

            var productCountAfterSpec = await _productRepository.GetCountAsync(productCount_Spec);

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
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var product = await _productRepository.GetByIdWithSpecAsync(spec);

            if (product is null)
                return NotFound();

            var productDTo = _mapper.Map<Product, ProductToReturn_Dto>(product);

            return Ok(productDTo);

        }


    }
}
