using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.API.Contracts.Outgoing;

namespace Authorization.Data.Repository;

public interface IServiceRepository : IRepositoryBase<Service>
{
    public Task<OutServicesDto[]> GetAllServiceAsync(CancellationToken cancellationToken);
    public Task<OutServicesDto?> GetServiceByIdAsync(long id, CancellationToken cancellationToken);

    public Task SetSpecializationAsync(ICollection<long> servicesId, long specializationId,
        CancellationToken cancellationToken);
}

public class ServiceRepository : RepositoryBase<Service>, IServiceRepository
{
    public ServiceRepository(HospitalDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<OutServicesDto[]> GetAllServiceAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Services.Select(x => new OutServicesDto
        {
            Id = x.Id,
            Title = x.Title,
            Price = x.Price,
            IsActive = x.IsActive,
            ServiceCategoryName = x.ServiceCategory.Title
        }).ToArrayAsync(cancellationToken);
    }

    public async Task<OutServicesDto?> GetServiceByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await DbContext.Services.Where(x => x.Id == id).Select(x => new OutServicesDto
        {
            Id = x.Id,
            Title = x.Title,
            Price = x.Price,
            IsActive = x.IsActive,
            ServiceCategoryName = x.ServiceCategory.Title
        }).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task SetSpecializationAsync(ICollection<long> servicesId, long specializationId,
        CancellationToken cancellationToken)
    {
        foreach (var id in servicesId)
        {
            var service = await GetAsync(id, cancellationToken);
            service.SpecializationId = specializationId;
            await UpdateAsync(service, cancellationToken);
        }
    }
}