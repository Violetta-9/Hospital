using Authorization.Data.Repository.Abstraction;
using System.Data.Entity;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;

namespace Authorization.Data.Repository
{
    public interface IDoctorRepository : IRepositoryBase<Doctor>
    {
        Doctor GetDoctorByAccountId(string accountId,
            CancellationToken cancellationToken = default);

    }

    internal class DoctorRepository : RepositoryBase<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public Doctor GetDoctorByAccountId(string accountId, CancellationToken cancellationToken = default)
        {

            return DbContext.Doctors.SingleOrDefault(x => x.AccountId == accountId);
        }
    }
}
