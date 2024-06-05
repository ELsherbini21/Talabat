using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    // this class contain method , this method build the query . 
    // This Query will Be Run Against Domain module .
    public static class SpecificationsEvaluator<T> where T : BaseEntity
    {
        // This method will Reutrn i Querybale of T [product , brand ]
        // input that we will our query , reporesent dbset . 
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> spec)
        {
            // any Query Start with Dbset .
            // Example => dbContext . Dbset<product>(); this is Iqueryable of product. 

            var Query = inputQuery;

            var specCriteria = spec.Criteria;

            if (specCriteria is not null) // cireture = p=>p.id
            {
                Query = Query.Where(specCriteria);
                // => Example : Query = Query.Set<Product>().where(p=>p.id==3);

                // then i want to accuminate or Aggeretage include at my Query . 


            }

            if (spec.OrderBy is not null && spec.OrderByDesc is null)
                Query = Query.OrderBy(spec.OrderBy);

            else if (spec.OrderByDesc is not null && spec.OrderBy is null)
                Query = Query.OrderByDescending(spec.OrderByDesc);


            if (spec.IsPaginationEnabled)
                Query = Query.Skip(spec.Skip).Take(spec.Take);

            Query = spec.Include
                   .Aggregate(Query, (currentQuery, includeExpression) =>
                   currentQuery.Include(includeExpression)
                   );

            // current query = dbContext.set<product>.where.
            // includeExpression .include . 
            // Then i want to add specification Add Query ,
            // specification such where , include 

            return Query;
        }
    }
}
