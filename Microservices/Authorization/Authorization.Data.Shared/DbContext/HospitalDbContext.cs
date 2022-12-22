using Authorization.Data.Shared.Interseptor;
using Authorization.Data_Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Data.Shared.DbContext
{
    public class HospitalDbContext:IdentityDbContext<Account>
    {
        
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options): base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     => optionsBuilder.AddInterceptors(new AuditableInterseptor());
    }
}
