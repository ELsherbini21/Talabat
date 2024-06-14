using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.Core.Product_Specs
{
    // container for common specifications between product and employee and order. 
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        // This constructor Will Be used for creating an object , that will be used to get all products .
        public ProductWithBrandAndCategorySpecifications(ProductSpecParams productSpecParams)
        : base(product =>

            (!productSpecParams.BrandId.HasValue ||
                product.ProductBrandId == productSpecParams.BrandId.Value)
        &&
            (!productSpecParams.CategoryId.HasValue ||
                product.ProductCategoryId == productSpecParams.CategoryId.Value)
        &&
            (string.IsNullOrEmpty(productSpecParams.ProductName) == true ||
                product.Name.ToLower().Contains(productSpecParams.ProductName))
        )
        {

            AddIncludes();

            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                if (productSpecParams.Sort.ToUpper() == "PriceAsc".ToUpper())
                    AddOrderBy(product => product.Price);
                else if (productSpecParams.Sort.ToUpper() == "PriceDesc".ToUpper())
                    AddOrderByDesc(product => product.Price);
                else
                    AddOrderBy(product => product.Name);
            }
            // make sorting in name .
            else
                AddOrderBy(product => product.Name);


            // here i wan't to apply pagination 
            //(total products =18)  ~20
            // pages size = 5 
            ApplyPagination((productSpecParams.PageIndex - 1) * productSpecParams.PageSize, productSpecParams.PageSize);
        }

        // This Constructor will be use for creating object , That will be use to get filter product with id .
        public ProductWithBrandAndCategorySpecifications(int id)
            : base(p => p.Id == id)
        {
            AddIncludes();
        }


        private void AddIncludes()
        {
            Include.Add(product => product.ProductBrand);

            Include.Add(product => product.ProductCategory);
        }

    }

}
