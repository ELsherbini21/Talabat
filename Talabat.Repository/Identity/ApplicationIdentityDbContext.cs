
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {

        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> dbContextOptions)
            : base(dbContextOptions)
        // call ONConfiguring . , i must make override it . to configure options that will be Sended .
        {

        }

        // I need to make change 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Address>().ToTable("Addresses");
        }
    }
}
