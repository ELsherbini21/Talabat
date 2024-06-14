using AutoMapper;
using Talabat.Apis.Dtos;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Apis.Helpers
{
    // Resolve when i Convert from OrderItem => OrderItem_Dto
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItem_Dto, string>
    {
        // Get Base Url From Configurations
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItem_Dto destination, string destMember, ResolutionContext context)
        {
            var sourceOfPicUrl = source.Product.PictureUrl;

            if (!string.IsNullOrEmpty(sourceOfPicUrl))
                return $"{_configuration["ApiBaseUrl"]}/{sourceOfPicUrl}";

            return string.Empty;
        }
    }
}
