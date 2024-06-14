using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications.OrderSpecs
{
    public class Order_Spec : BaseSpecifications<Order>
    {
        public Order_Spec(string buyerEmail)
            : base(order => order.BuyerEmail.ToLower() == buyerEmail.ToLower())
        {

            AddIncludes();

            AddOrderByDesc(order => order.OrderDate);

        }

        public Order_Spec(int orderId, string buyerEmail)// chain on parameterized ctor of base .
          : base(order => order.BuyerEmail.ToLower() == buyerEmail.ToLower() && order.Id == orderId)
        {

            AddIncludes();

            //AddOrderByDesc(order => order.OrderDate); ==> Only one order . 

        }


        private void AddIncludes()
        {
            Include.Add(order => order.DeliveryMethod);

            Include.Add(order => order.OrderItems);
        }
    }
}
