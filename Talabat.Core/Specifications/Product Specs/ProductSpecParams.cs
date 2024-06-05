using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductSpecParams
    {
        private const int maxPageSize = 10;

        private int pageSize = 5; // number of item in one page 

        public int PageSize
        {
            get => pageSize;

            set => pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        public int PageIndex { get; set; } = 1;  // Each Page contain Number of Elements ;

        public string? Sort { get; set; }

        public int? BrandId { get; set; }

        public int? CategoryId { get; set; }

        private string? productName;

        public string? ProductName
        {
            get =>productName?.ToLower();
            set { productName = value?.ToLower(); }
        }

    }
}
