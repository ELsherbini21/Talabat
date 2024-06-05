namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }


        public virtual ProductBrand ProductBrand { get; set; } // Navigation Property [one]
        public int ProductBrandId { get; set; } // F.K Product brand . 


        public virtual ProductCategory ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
