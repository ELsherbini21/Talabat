using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Controllers;
using Talabat.Apis.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order_Aggregate;

using IdentityAddress = Talabat.Core.Entities.Identity.Address;

using AggregateAddress = Talabat.Core.Entities.Order_Aggregate.Address;

namespace Talabat.Apis.Helpers
{
    // this class must inherit from profile .
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<IdentityAddress, Address_Dto>().ReverseMap();

            CreateMap<Address_Dto, AggregateAddress>(); // for order . 

            // destination == Target == dto .map to .
            // mapTo == Dto && MapFrom == Entity

            // Product => ProductToReturn_DTo
            CreateMap<Product, ProductToReturn_Dto>()
                .ForMember(productDto => productDto.BrandName, option => option
                    .MapFrom(product => product.ProductBrand.Name))
                .ForMember(dto => dto.CategoryName, option => option
                    .MapFrom(product => product.ProductCategory.Name))
                // i make this because in inject Pro 
                .ForMember(dto => dto.PictureUrl, option => option
                .MapFrom<ProductPictureUrlResolver>());

            // Order => OrderToReturn_Dto 
            CreateMap<Order, OrderToReturn_Dto>()
                .ForMember(option => option.DeliveryMethod, option => option
                    .MapFrom(order => order.DeliveryMethod.ShortName))
                .ForMember(option => option.DeliveryMethodCost, option => option
                    .MapFrom(order => order.DeliveryMethod.Cost));

            // OrderItem => OrderItem_Item
            CreateMap<OrderItem, OrderItem_Dto>()
                .ForMember(d => d.ProductId, option => option.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, option => option.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, option => option.MapFrom(s => s.Product.PictureUrl))
                .ForMember(d => d.PictureUrl, option => option.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<ProductBrand, ProductBrandToReturn_Dto>();

            CreateMap<ProductCategory, ProductCategoryToReturn_Dto>();

            CreateMap<CustomerBasket_Dto, CustomerBasket>();

            CreateMap<BasketItems_Dto, BasketItems>();

            CreateMap<ApplicationUser, ApplicationUser_Dto>();
        }

    }
}
