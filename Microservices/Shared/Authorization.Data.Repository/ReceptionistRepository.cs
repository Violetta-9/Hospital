using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Profile.Application.Contracts.Outgoing;

namespace Authorization.Data.Repository;

public interface IReceptionistRepository : IRepositoryBase<Receptionist>
{
    Task<Receptionist> GetReceptionistByAccountIdAsync(string accountId,
        CancellationToken cancellationToken = default);

    public Task<ReceptionistAllDTO[]> GetAllReceptionistAsync(CancellationToken cancellationToken = default);

    public Task<ReceptionistOneDTO?> GetReceptionistByIdAsync(long id,
        CancellationToken cancellationToken = default);
}

internal class ReceptionistRepository : RepositoryBase<Receptionist>, IReceptionistRepository
{
    public ReceptionistRepository(HospitalDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Receptionist> GetReceptionistByAccountIdAsync(string accountId,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Receptionists.SingleOrDefaultAsync(x => x.AccountId == accountId);
    }

    public async Task<ReceptionistAllDTO[]> GetAllReceptionistAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Receptionists.Select(x => new ReceptionistAllDTO
        {
            Id = x.Id,
            FirstName = x.Account.FirstName,
            LastName = x.Account.LastName,
            MiddleName = x.Account.MiddleName,
            OfficeAddress = x.Office.Address
        }).ToArrayAsync(cancellationToken);
    }

    public async Task<ReceptionistOneDTO?> GetReceptionistByIdAsync(long id,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Receptionists.Where(x => x.Id == id).Select(x => new ReceptionistOneDTO
        {
            AccountId = x.AccountId,
            FirstName = x.Account.FirstName,
            LastName = x.Account.LastName,
            MiddleName = x.Account.MiddleName,
            OfficeAddress = x.Office.Address
        }).SingleOrDefaultAsync(cancellationToken);
    }
}