using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Diagnostics;
using Talabat.Apis.Dtos;
using Talabat.Apis.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Apis.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepo, IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }

        [HttpGet]//Get : baseUrl \ api \ basket?id .
        public async Task<ActionResult<CustomerBasket>> GetById(string id)
        {
            var basket = await _basketRepo.GetBasketAsyncById(id);
            // if basket = null , that mean it's expired .
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateOrCreate([FromBody] CustomerBasket_Dto basketDto)
        {
            var basket = _mapper.Map<CustomerBasket_Dto, CustomerBasket>(basketDto);

            var result = await _basketRepo.CreateOrUpdateBasketAsync(basket);

            if (result is null)
                return BadRequest(new ApiResponse(400));
            else
                return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<CustomerBasket>> Delete(string id)
        {
            var result = await _basketRepo.DeleteBasketAsync(id);

            return (result is true) ? Ok("Deleted Process has been completed ") : BadRequest(new ApiResponse(404));
        }




    }

}
