using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext dbContext)
        {
            // here i must Check if the table don't contain any data .
            if (dbContext.ProductBrands.Count() == 0)
            {
                var productBrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var productBrandsObjects = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);

                if (productBrandsObjects?.Count() > 0)
                {
                    // here i make iterate at each brand .
                    //productBrandsObjects = productBrandsObjects.Select(brand => new ProductBrand()
                    //{
                    //    Name = brand.Name,

                    //}).ToList(); 
                    foreach (var productBrand in productBrandsObjects)
                        dbContext.Set<ProductBrand>().Add(productBrand);

                    await dbContext.SaveChangesAsync();

                }
            }

            if (dbContext.ProductCategories.Count() == 0)
            {
                var productCategoryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/categories.json");
                var productCategoryObjects = JsonSerializer.Deserialize<List<ProductCategory>>(productCategoryData);

                if (productCategoryObjects?.Count() > 0)
                {
                    foreach (var productCategory in productCategoryObjects)
                        dbContext.Set<ProductCategory>().Add(productCategory);

                    await dbContext.SaveChangesAsync();
                }
            }

            if (dbContext.Products.Count() == 0)
            {
                var ProductsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var ProductObjects = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (ProductObjects?.Count() > 0)
                {
                    foreach (var product in ProductObjects)
                        dbContext.Products.Add(product);

                    await dbContext.SaveChangesAsync();
                }

            }

            if (dbContext.DeliveryMethods.Count() == 0)
            {
                var delivery = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var deliveryMethodsData = JsonSerializer.Deserialize<List<DeliveryMethod>>(delivery);

                if (deliveryMethodsData?.Count() > 0)
                {
                    foreach (var deliver in deliveryMethodsData)
                        dbContext.DeliveryMethods.Add(deliver);

                    await dbContext.SaveChangesAsync();
                }

            }

        }
    }
}
