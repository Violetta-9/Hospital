using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;

namespace Authorization.Data.Repository;

public interface IDocumentsRepository : IRepositoryBase<Photo>
{
}

public class DocumentsRepository : RepositoryBase<Photo>, IDocumentsRepository
{
    public DocumentsRepository(HospitalDbContext dbContext) : base(dbContext)
    {
    }
}