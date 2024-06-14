using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Core
{
    public interface IUnitOfWork : IDisposable
    {
        // here i will make method . this method return IGenericReopsitory of T .
        // Repostiory<Product> ==> return IGenericRepo<Product>
        IGenericRepository<T> Repository<T>() where T : BaseEntity;

        Task<int> SaveChangesAsync(); // return number of rows that effected .
    }

    // i interact with IGenericRepo because i don't have repo for specific module .
}
