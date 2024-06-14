using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Contract
{
    // type paramter must be from class base entity , or any type inherit from it .
    // i make interface is Generic , And i make Constraint at T .
    public interface IGenericRepository<T> where T : BaseEntity
    {
        // this method will return Task of T .
        // because i work async /
        Task<T?> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T?> GetByIdWithSpecAsync(ISpecifications<T> spec);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

        Task<int> GetCountAsync(ISpecifications<T> specifications);


        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
