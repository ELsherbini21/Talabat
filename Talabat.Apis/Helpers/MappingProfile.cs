using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Controllers;
using Talabat.Apis.Dtos;
using Talabat.Core.Entities;

namespace Talabat.Apis.Helpers
{
    // this class must inherit from profile .
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {

            // destination == Target == dto .map to .
            // mapTo == Dto && MapFrom == Entity

            CreateMap<Product, ProductToReturn_Dto>()
                .ForMember(productDto => productDto.BrandName,
                    option => option.MapFrom(product => product.ProductBrand.Name))
                .ForMember(dto => dto.CategoryName,
                    option => option.MapFrom(product => product.ProductCategory.Name))
                // i make this because in inject Pro 
                .ForMember(dto => dto.PictureUrl,
                option => option.MapFrom<ProductPictureUrlResolver>());


            CreateMap<ProductBrand, ProductBrandToReturn_Dto>();

            CreateMap<ProductCategory, ProductCategoryToReturn_Dto>();
        }

    }
}
