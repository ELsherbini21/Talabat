using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Talabat.Apis.Dtos
{
    public class BasketItems_Dto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductPictureUrl { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string BrandName { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "price must be greater than zero !")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must at least one item!")]
        public int Quantity { get; set; }
    }
}
