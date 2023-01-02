using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;
using Profile.Application.Contracts.Outgoing;
using Microsoft.EntityFrameworkCore;


namespace Authorization.Data.Repository
{
    public interface IPatientRepository : IRepositoryBase<Patient>
    {
        Task<Patient> GetPatientByAccountId(string accountId,
            CancellationToken cancellationToken = default);

        public Task<PatientAllDTO[]> GetAllPatientsAsync(CancellationToken cancellationToken = default);
        public Task<PatientOneDTO?> GetPatientByIdAsync(long id, CancellationToken cancellationToken = default);
    }

    internal class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        public PatientRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Patient> GetPatientByAccountId(string accountId, CancellationToken cancellationToken = default)
        {
            return await DbContext.Patients.SingleOrDefaultAsync(x => x.AccountId == accountId);
        }
        public async Task<PatientAllDTO[]> GetAllPatientsAsync(CancellationToken cancellationToken = default)
        {
            return await DbContext.Patients.Select(x => new PatientAllDTO()
            {
                Id = x.Id,
                FirstName = x.Account.FirstName,
                LastName = x.Account.LastName,
                MiddleName = x.Account.MiddleName,
                PhoneNumber = x.Account.PhoneNumber,
               

            }).ToArrayAsync(cancellationToken);

        }
        public async Task<PatientOneDTO?> GetPatientByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Patients.Where(x => x.Id == id).Select(x => new PatientOneDTO()
            {
                AccountId = x.AccountId,
                FirstName = x.Account.FirstName,
                LastName = x.Account.LastName,
                MiddleName = x.Account.MiddleName,
                PhoneNumber = x.Account.PhoneNumber,
                BirthDay = x.Account.Birthday,
                
              

            }).SingleOrDefaultAsync(cancellationToken);

        }

    }
}
