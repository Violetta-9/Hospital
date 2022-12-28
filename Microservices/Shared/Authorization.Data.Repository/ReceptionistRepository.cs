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
    public interface IReceptionistRepository : IRepositoryBase<Receptionist>
    {
        Receptionist GetReceptionistByAccountId(string accountId,
            CancellationToken cancellationToken = default);

    }

    internal class ReceptionistRepository : RepositoryBase<Receptionist>, IReceptionistRepository
    {
        public ReceptionistRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public Receptionist GetReceptionistByAccountId(string accountId, CancellationToken cancellationToken = default)
        {
            return DbContext.Receptionists.SingleOrDefault(x => x.AccountId == accountId);
        }
    }
}
