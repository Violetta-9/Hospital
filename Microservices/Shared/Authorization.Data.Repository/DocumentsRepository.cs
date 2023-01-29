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
    public interface IDocumentsRepository : IRepositoryBase<Documentation>
    {

    }
    public class DocumentsRepository : RepositoryBase<Documentation>, IDocumentsRepository
    {
        public DocumentsRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }


}
