using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Data.Repository;

public interface IStatusRepository : IRepositoryBase<Status>
{
    public Task<long> GetStatusIdByNameAsync(string name, CancellationToken cancellationToken = default);
}

public class StatusRepository : RepositoryBase<Status>, IStatusRepository
{
    public StatusRepository(HospitalDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<long> GetStatusIdByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await DbContext.Statuses.Where(x => x.Title.ToLower() == name.ToLower()).Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}