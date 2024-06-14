using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Talabat.Apis.Dtos;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Apis.Params
{
    public class OrderCreationParams
    {

        [Required]
        public string BasketId { get; set; }

        [Required]
        public int DeliveryMethodId { get; set; }

        [Required]
        public Address_Dto ShippingAddress { get; set; }
    }
}
