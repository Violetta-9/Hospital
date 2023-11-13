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


    public Task<OutServicesDto[]> GetServiceBySpecializationIdAsync(long specializationId,
        CancellationToken cancellationToken = default);

    public Task<bool> IsServiceContainsFreeSpecializationAsync(long specializationId,
        CancellationToken cancellationToken);

    public Task<Service[]> GetAllFreeServiceAsync(CancellationToken cancellationToken);

    public Task UpdateSpecializationIdAsync(ICollection<long> servicesId, long specializationId,
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

    public async Task<Service[]> GetAllFreeServiceAsync(CancellationToken cancellationToken)
    {
       return await DbContext.Services.Where(x => x.SpecializationId == null).ToArrayAsync(cancellationToken);
    }

    public async Task UpdateSpecializationIdAsync(ICollection<long> servicesId, long specializationId,
        CancellationToken cancellationToken)
    {
        var listOfSpecialization =
            await GetServiceBySpecializationIdAsync(specializationId, cancellationToken);
        if (listOfSpecialization != null)
        {
            var listOfServicesDB = listOfSpecialization.Select(x => x.Id).ToArray();
            var result = listOfServicesDB.Except(servicesId).ToArray();
            if (result.Any()) await DeleteSpecializationIdAsync(result, cancellationToken);
        }

        await SetSpecializationAsync(servicesId, specializationId, cancellationToken);
    }

    public async Task<OutServicesDto[]> GetServiceBySpecializationIdAsync(long specializationId,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Services.Where(x => x.SpecializationId == specializationId).Select(x =>
            new OutServicesDto
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                IsActive = x.IsActive,
                ServiceCategoryName = x.ServiceCategory.Title
            }).ToArrayAsync(cancellationToken);
    }

    public async Task<bool> IsServiceContainsFreeSpecializationAsync(long specializationId,
        CancellationToken cancellationToken)
    {
        return await DbContext.Services.Where(x => x.Id == specializationId && x.SpecializationId == null)
            .AnyAsync(cancellationToken);
    }

    private async Task DeleteSpecializationIdAsync(ICollection<long> servicesId,
        CancellationToken cancellationToken)
    {
        foreach (var id in servicesId)
        {
            var service = await GetAsync(id, cancellationToken);
            service.SpecializationId = null;
            await UpdateAsync(service, cancellationToken);
        }
    }
}