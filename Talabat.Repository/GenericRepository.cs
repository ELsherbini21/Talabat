using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    // here T for Interface is From type base Entity
    // And T for class is type Parameter , so that i must make constraint at it .
    // Base Entity in this class is 
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _storeContext;

        // ask Clr for creating object from class storecontext implicilty . 
        // or ask IOC container to resolve object from storeContext Service .
        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        //2 . if there are method don't have navigation property . 
        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _storeContext.Set<T>().ToListAsync();

        // here because return may be object or null 
        // so the return must be nullable T 
        public async Task<T?> GetByIdAsync(int id)
            => await _storeContext.Set<T>().FindAsync(id);

        public async Task<T?> GetByIdWithSpecAsync(ISpecifications<T> spec)
            => await ApplySpec(spec).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
            => await ApplySpec(spec).ToListAsync();

        public async Task<int> GetCountAsync(ISpecifications<T> specifications)
            => await ApplySpec(specifications).CountAsync();



        private IQueryable<T> ApplySpec(ISpecifications<T> spec)
             => SpecificationsEvaluator<T>.GetQuery(_storeContext.Set<T>(), spec);


    }
}
