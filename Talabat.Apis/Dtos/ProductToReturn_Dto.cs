using Talabat.Core.Entities;

namespace Talabat.Apis.Dtos
{
    public class ProductToReturn_Dto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }


        public string BrandName { get; set; } // Navigation Property [one]
        public int ProductBrandId { get; set; } // F.K Product brand . 

        public string CategoryName { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
