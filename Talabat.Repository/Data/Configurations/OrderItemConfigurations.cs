﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.Configurations
{
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // will mapped to owner .
            builder.OwnsOne(orderItem => orderItem.Product , product=>product.WithOwner());

            builder.Property(orderItem => orderItem.Price)
                .HasColumnType("decimal(18,4)");
        
        }
    }
}
