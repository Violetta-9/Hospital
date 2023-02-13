using Appointment.API.Application.Contracts.Outgoing;
using Authorization.Data.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data_Domain.Models;
using Authorization.Data.Shared.DbContext;

namespace Authorization.Data.Repository
{
    public interface IResultRepository : IRepositoryBase<Result>
    {
       

    }
    public class ResultRepository : RepositoryBase<Result>, IResultRepository
    {
        public ResultRepository(HospitalDbContext dbContext) : base(dbContext)
        {
        }
    }
}
