using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Claims;
using Talabat.Apis.Dtos;
using Talabat.Apis.Errors;
using Talabat.Apis.Params;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services.Contract;

namespace Talabat.Apis.Controllers
{
    [Authorize] //default = bearer .
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _service;
        private readonly IMapper _mapper;

        public OrderController(IOrderService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        #region Create Order 

        // Imporiving Swagger .
        [ProducesResponseType(typeof(OrderToReturn_Dto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturn_Dto>> Create(OrderCreationParams orderParams)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var address = _mapper.Map<Address_Dto, Address>(orderParams.ShippingAddress);

            var resultOfOrderCreating = await _service.CreateOrderAsync(userEmail, orderParams.BasketId, orderParams.DeliveryMethodId, address);

            if (resultOfOrderCreating is not null)
            {
                var orderToReturn = _mapper.Map<Order, OrderToReturn_Dto>(resultOfOrderCreating);

                return Ok(orderToReturn);
            }

            return BadRequest(new ApiResponse(400));
        }

        #endregion

  
        #region  Get All Orders For Specific Users .

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturn_Dto>>> GetOrdersForUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var orders = await _service.GetOrderForUserAsync(userEmail);

            if (orders?.Count() > 0)
            {
                var ordersToReturn = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturn_Dto>>(orders);

                return Ok(ordersToReturn);
            }

            return BadRequest(new ApiResponse(404));

        }

        #endregion


        #region Get Specific Order for specific User . 

        [ProducesResponseType(typeof(OrderToReturn_Dto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("orderId")]
        public async Task<ActionResult<OrderToReturn_Dto>> GetSpecificOrderForUser(int orderId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var order = await _service.GetSpecificOrderByIdForUserAsync(orderId, userEmail);

            if (order is not null)
            {
                var orderToReturn = _mapper.Map<Order, OrderToReturn_Dto>(order);

                return Ok(orderToReturn);
            }

            return NotFound(new ApiResponse(404));

        }

        #endregion


        #region Get All Delivery Methods 

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveryMethods()
        {
            var deliveryMethods = await _service.GetAllDeliveryMethodsAsync();

            return (deliveryMethods is not null) ? Ok(deliveryMethods) : BadRequest(new ApiResponse(404));
        }

        #endregion
    }

}
