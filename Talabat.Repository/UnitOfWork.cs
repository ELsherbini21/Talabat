using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    // this class interact with Database through dbContext .
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        // any Class That Inerity From BaseEntity.

        private Hashtable _repos;

        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            // Open Closed Princible . 
            _repos = new Hashtable();
        }

        // create Repo Per Request .
        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            // when he calling this method for first time => _repos = null; 

            // checking the Repo Request Exist before or not . 

            var key = typeof(T).Name;

            if (!_repos.ContainsKey(key)) // if dictionary don't contain key , i will create object .
            {
                var repoOfT = new GenericRepository<T>(_storeContext);

                _repos.Add(key, repoOfT);
            }

            return _repos[key] as IGenericRepository<T>;

        }

        public async Task<int> SaveChangesAsync() => await _storeContext.SaveChangesAsync();

        public async void Dispose() => await _storeContext.DisposeAsync();

    }


}
