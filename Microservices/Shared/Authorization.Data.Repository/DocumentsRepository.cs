using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;

namespace Authorization.Data.Repository;

public interface IDocumentsRepository : IRepositoryBase<Document>
{
}

public class DocumentsRepository : RepositoryBase<Document>, IDocumentsRepository
{
    public DocumentsRepository(HospitalDbContext dbContext) : base(dbContext)
    {
    }
}