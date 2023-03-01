using Microsoft.EntityFrameworkCore;
using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;
using Services.API.Contracts.Outgoing;

namespace Authorization.Data.Repository;

public interface IServiceCategoryRepository : IRepositoryBase<ServiceCategory>
{
    public Task<ServiceCategoriesDTO[]> GetServiceCategories(CancellationToken cancellationToken = default);
}

public class ServiceCategoryRepository : RepositoryBase<ServiceCategory>, IServiceCategoryRepository
{
    public ServiceCategoryRepository(HospitalDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<ServiceCategoriesDTO[]> GetServiceCategories(CancellationToken cancellationToken = default)
    {
        return await DbContext.ServiceCategories.Select(x => new ServiceCategoriesDTO()
        {
            Id=x.Id,
            Title = x.Title,
            TimeSlotSize = x.TimeSlotSize,
        }).ToArrayAsync(cancellationToken);
    }
}