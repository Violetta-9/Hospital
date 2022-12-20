using Authorization.Data.Shared.Interseptor;
using Authorization.Data_Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Data.Shared.DbContext
{
    public class HospitalDbContext:IdentityDbContext<Account>
    {
        public DbSet<Photo> Photos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     => optionsBuilder.AddInterceptors(new AuditableInterseptor());
    }
}
