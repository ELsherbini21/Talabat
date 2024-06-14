using System.Linq.Expressions;
using System.Reflection;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    // this class represent specification of Query That will be runed at dbSet of T .
    // T Represent DbSet<T>
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        // automatic Property .
        public Expression<Func<T, bool>> Criteria { get; set; }

        // if the method  that i will create object from this class to it .

        // => . this method will get all elemtns => critera = null 
        // why ? example dbset<T>().GetAll()

        // => . this method will get Filter Elements => critera != null 
        // why ? example dbset<T>().GetAll().where (P=>p.id ==1) 
        // here criteria Equals [p.id == id ]

        // i have list of include .

        //becasue the value of propetry is identitcal in all ctor 
        // i can assign it  at property signature .
        public List<Expression<Func<T, object>>> Include { get; set; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; set; } // orderby(t=>t.obj)

        public Expression<Func<T, object>> OrderByDesc { get; set; }


        public int Skip { get; set; }

        public int Take { get; set; }

        public bool IsPaginationEnabled { get; set; } = false; // default value , for not pagitation that wasn't enbald

        // when Criteria is null ,i want build Query Get All Elelmts .
        public BaseSpecifications()
        {
            // let criteria = null ;

        }

        // this ctor take expression , becasuse  i want to get fileter elements 
        // example : where (expression ) => where (p.id== id ) 
        public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }


        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }

        public void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled = true;

            Skip = skip;

            Take = take;
        }

    }
}
