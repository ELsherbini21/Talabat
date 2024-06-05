namespace Talabat.Core.Entities
{
    public class ProductBrand : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Product> products { get; set; } = new HashSet<Product>();
    }
}
