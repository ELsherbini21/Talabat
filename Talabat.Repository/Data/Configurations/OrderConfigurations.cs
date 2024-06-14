using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Entities.Order_Aggregate.Enum;

namespace Talabat.Repository.Data.Configurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(order => order.ShippingAddress, sihppingAddress => sihppingAddress.WithOwner());

            builder.Property(order => order.Status)
                .HasConversion(orderStatus => orderStatus.ToString(), // Value In DataBase .
                orderStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), orderStatus));  //In applicaion .

            builder.Property(order => order.Subtotal)
                .HasColumnType("decimal(18,4)");

            builder.Property(order => order.DeliveryMethodId)
                .IsRequired(false);

            builder.HasOne(order => order.DeliveryMethod)
                .WithMany()
                .HasForeignKey(order => order.DeliveryMethodId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
