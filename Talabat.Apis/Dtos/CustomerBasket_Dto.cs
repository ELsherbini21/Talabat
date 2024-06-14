using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.Apis.Dtos
{
    public class CustomerBasket_Dto
    {
        [Required]
        public string Id { get; set; }


        public List<BasketItems_Dto> Items { get; set; }
    }
}
