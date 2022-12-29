using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Profile.Application.Contracts.Outgoing;

namespace Authorization.Data.Repository
{
    public interface IDoctorRepository : IRepositoryBase<Doctor>
    {
       Task<Doctor> GetDoctorByAccountIdAsync(string accountId,
            CancellationToken cancellationToken = default);

       public Task<DoctorAllDTO[]> GetAllDoctors(CancellationToken cancellationToken = default);
       public Task<DoctorOneDTO?> GetDoctorById(long id, CancellationToken cancellationToken = default);

    }

    internal class DoctorRepository : RepositoryBase<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Doctor> GetDoctorByAccountIdAsync(string accountId, CancellationToken cancellationToken = default)
        {

            return await DbContext.Doctors.SingleOrDefaultAsync(x => x.AccountId == accountId,cancellationToken);
        }
        public async Task<DoctorAllDTO[]> GetAllDoctors(CancellationToken cancellationToken = default)
        {
            return await DbContext.Doctors.Select(x => new DoctorAllDTO()
            {
                Id=x.Id,
                FirstName = x.Account.FirstName,
                LastName = x.Account.LastName,
                MiddleName = x.Account.MiddleName,
                BirthDay = x.Account.Birtday,
                OfficeAddress = x.Office.Address,
                SpecializationTitle = x.Specialization.Title,
                StatusTitle = x.Status.Title

            }).ToArrayAsync(cancellationToken);

        }
        public async Task<DoctorOneDTO?> GetDoctorById(long id,CancellationToken cancellationToken = default)
        {
            return await DbContext.Doctors.Where(x=>x.Id==id).Select(x => new DoctorOneDTO()
            {
                AccountId = x.AccountId,
                FirstName = x.Account.FirstName,
                LastName = x.Account.LastName,
                MiddleName = x.Account.MiddleName,
                BirthDay = x.Account.Birtday,
                OfficeAddress = x.Office.Address,
                SpecializationTitle = x.Specialization.Title,
                StatusTitle = x.Status.Title,
                CareerStartYear = x.CareerStartYear

            }).FirstOrDefaultAsync(cancellationToken);

        }
    }
}
