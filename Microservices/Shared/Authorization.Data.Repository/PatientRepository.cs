using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Data.Repository
{
    public interface IPatientRepository : IRepositoryBase<Patient>
    {
        Patient GetPatientByAccountId(string accountId,
            CancellationToken cancellationToken = default);
    }

    internal class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        public PatientRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public Patient GetPatientByAccountId(string accountId, CancellationToken cancellationToken = default)
        {
            return DbContext.Patients.SingleOrDefault(x => x.AccountId == accountId);
        }

    }
}
