using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;
using Services.API.Contracts.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Data.Repository
{
    public interface IServiceCategoryRepository : IRepositoryBase<ServiceCategory>
    {
      
    }

    public class ServiceCategoryRepository : RepositoryBase<ServiceCategory>, IServiceCategoryRepository
    {
        public ServiceCategoryRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

       
    }
   
}
