using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;
using Services.API.Contracts.Outgoing;
using Specialization.API.Application.Contracts.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpecializationEntity= Authorization.Data_Domain.Models.Specialization;
namespace Authorization.Data.Repository
{
    
    public interface ISpecializationRepository : IRepositoryBase<SpecializationEntity>
    {
        public Task<SpecializationListDTO[]> GetAllSpecializationAsync(CancellationToken cancellationToken);
        public Task<SpecializationDTO?> GetSpecializationByIdAsync(long id, CancellationToken cancellationToken);
        
        }

    public class SpecializationRepository : RepositoryBase<SpecializationEntity>, ISpecializationRepository
    {
        public SpecializationRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<SpecializationListDTO[]> GetAllSpecializationAsync(CancellationToken cancellationToken)
        {
            return await DbContext.Specializations.Select(x => new SpecializationListDTO()
            {
                Id = x.Id,
                Title = x.Title,
               IsActive = x.IsActive,
              
            }).ToArrayAsync(cancellationToken);
        }
        public async Task<SpecializationDTO?> GetSpecializationByIdAsync(long id,CancellationToken cancellationToken)
        {
            return await DbContext.Specializations.Where(x=>x.Id==id).Select(x => new SpecializationDTO()
            {
                Id = x.Id,
                Title = x.Title,
                IsActive = x.IsActive,

            }).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
