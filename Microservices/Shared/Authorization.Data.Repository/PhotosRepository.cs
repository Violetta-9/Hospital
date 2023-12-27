using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;

namespace Authorization.Data.Repository;

public interface IPhotosRepository : IRepositoryBase<Photo>
{
}

public class PhotosRepository : RepositoryBase<Photo>, IPhotosRepository
{
    public PhotosRepository(HospitalDbContext dbContext) : base(dbContext)
    {
    }
}