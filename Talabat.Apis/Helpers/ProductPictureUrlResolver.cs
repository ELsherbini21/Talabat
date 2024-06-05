using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Talabat.Apis.Dtos;
using Talabat.Core.Entities;

namespace Talabat.Apis.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturn_Dto, string>
    // string refer to picture url . 
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturn_Dto destination, string destMember, ResolutionContext context)
        {
            var sourceOfPicUrl = source.PictureUrl;

            if (!string.IsNullOrEmpty(sourceOfPicUrl))
                return $"{_configuration["ApiBaseUrl"]}/{sourceOfPicUrl}";

            return string.Empty;
        }
    }
}
