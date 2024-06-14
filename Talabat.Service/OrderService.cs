using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.OrderSpecs;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepo;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo)
        {
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
        }

        #region Get All Orders 
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            // 1. Get Basket From Baskets Repo . 
            var basket = await _basketRepo.GetBasketAsyncById(basketId);

            if (basket?.Items?.Count() > 0) // Check Items in Basket .
            {
                // 2. Get Selected Items at basket from products . [productId , Quantity .]
                var orderItems = new List<OrderItem>();

                var productRepo = _unitOfWork.Repository<Product>();//Exec ths method only once .

                foreach (var item in basket.Items)
                {
                    // get The Real Details of product based on Id That Exist in Basket .  [ID && Quantity]
                    var productFromDataBase = await productRepo.GetByIdAsync(item.Id);

                    if (productFromDataBase is not null)
                    {
                        // i get details from database To ensure that Data is real Data .
                        var productItemOrder =
                            new ProductItemOrder(productFromDataBase.Id, productFromDataBase.Name, productFromDataBase.PictureUrl);


                        var orderItem = new OrderItem(productItemOrder, productFromDataBase.Price, item.Quantity);

                        orderItems.Add(orderItem);
                        //orderItems.Add(new OrderItem(, productFromDataBase?.Price, item.Quantity));
                    }


                }
                // 3. calculate subTotal .
                var subTotal = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);

                // 4. Get delivery method from DeliveryMethod repo . 
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

                if (deliveryMethod is not null)
                {
                    // 5. Create Order 
                    var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal);

                    await _unitOfWork.Repository<Order>().AddAsync(order);

                    // 6 Save To DataBase
                    var resultOfSaving = await _unitOfWork.SaveChangesAsync();

                    if (resultOfSaving > 0)
                        return order;
                    else
                        return null;

                }
            }

            return null;

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
        {
            var deliveryMethodsData = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

            if (deliveryMethodsData?.Count() > 0)
                return deliveryMethodsData;
            else
                return null;


        }

        #endregion


        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var spec = new Order_Spec(buyerEmail);

            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec); // here i set repo .

            if (orders?.Count() > 0)
                return orders;

            else
                return null;
        }


        public async Task<Order?> GetSpecificOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var spec = new Order_Spec(orderId, buyerEmail);

            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);

            if (order is not null) return order;

            else return null;
        }



    }
}
