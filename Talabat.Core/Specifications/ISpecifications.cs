using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    // this Specifications is Related For Query . 
    // This Query Will Run Against DbSet<T> .
    // This specifications Used For Build Query .

    // the Template of Sepcification Object That i Will Send To method That will Build The query .
    public interface ISpecifications<T> where T : BaseEntity
    {
        // iside this interface : i will make Signature for Each [ Spec || Query ]


        // 1. ==> where Spec <===
        // here i make signature for Where () , where take Lambda Expression .
        // exmaple => spec refer to The Querye Operator and it's parameter .


        // this Property Contain Value of spec , where (value OF speck here.)
        // Crieter => the value that i will send to Linq Operator such Where(crietrai value .)
        // The Expression || Criterai of linq operator is Fat arrow Function .
        public Expression<Func<T, bool>> Criteria { get; set; } // this vairable that contain value that i will send to where , Such   (value) value = p => p.id == i23 ;



        // 2. ==> Include <==
        // i have many InClude . 

        public List<Expression<Func<T, object>>> Include { get; set; }

        // include (p=>p.Brand) : this method return object : it's Parent type 
        // becasue once return ProductBrand || productCategory .


        public Expression<Func<T, object>> OrderBy { get; set; } // orderby(t=>t.obj)

        public Expression<Func<T, object>> OrderByDesc { get; set; } // orderby(t=>t.obj)

        public int Skip { get; set; }
        public int Take { get; set; }

        public bool IsPaginationEnabled { get; set; }
        //(True) ? take && skip: don't apply pagination ;
    }
}
