using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductWithFiltrationForCount_Spec : BaseSpecifications<Product>
    {
        public ProductWithFiltrationForCount_Spec(ProductSpecParams productSpecParams)
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

        }
    }
}
