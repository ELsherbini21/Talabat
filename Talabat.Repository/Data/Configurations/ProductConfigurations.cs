using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(entity => entity.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(entity => entity.Description).IsRequired();

            builder.Property(entity => entity.PictureUrl).IsRequired();

            builder.Property(entity => entity.Price)
                .HasColumnType("decimal(18,3)").IsRequired();

            #region Realtionship Configurations.
            // product(M)_________(1)ProductBrand 
            builder.HasOne(product => product.ProductBrand)
                .WithMany(productBrand => productBrand.products)
                .HasForeignKey(product => product.ProductBrandId);

            // product(M)_________(1)ProductCategory 
            builder.HasOne(product => product.ProductCategory)
                .WithMany(productCategory => productCategory.products)
                .HasForeignKey(product => product.ProductCategoryId);
            #endregion


        }
    }
}
