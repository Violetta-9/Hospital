using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;

namespace Authorization.Data.Repository;

public interface IServiceCategoryRepository : IRepositoryBase<ServiceCategory>
{
}

public class ServiceCategoryRepository : RepositoryBase<ServiceCategory>, IServiceCategoryRepository
{
    public ServiceCategoryRepository(HospitalDbContext dbContext) : base(dbContext)
    {
    }
}