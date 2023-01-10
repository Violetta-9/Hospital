using Authorization.Data.Repository.Abstraction;
using Authorization.Data_Domain.Models;
using Profile.Application.Contracts.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Shared.DbContext;
using Office.Application.Contracts.Outgoing;
using OfficeEntity= Authorization.Data_Domain.Models.Office;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Data.Repository
{
    public interface IOfficeRepository : IRepositoryBase<OfficeEntity>
    {
        public Task<AllOfficesDto[]> GetAllOfficesAsync(CancellationToken cancellationToken);
    }
    public class OfficeRepository: RepositoryBase<OfficeEntity>,IOfficeRepository
    {
        public OfficeRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<AllOfficesDto[]> GetAllOfficesAsync(CancellationToken cancellationToken)
        {
            return await DbContext.Offices.Select(x => new AllOfficesDto()
            {
                Address = x.Address,
                RegistryPhoneNumber = x.RegistryPhoneNumber,
                IsActive = x.IsActive
                
            }).ToArrayAsync(cancellationToken);
        }
    }
    
}
