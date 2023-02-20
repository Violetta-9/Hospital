using Authorization.Data_Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;

using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Status.GetStatus
{
    internal class GetStatusQueryHandler : IRequestHandler<GetStatusQuery, StatusDTO[]>
    {
        private readonly IStatusRepository _statusRepository;

        public GetStatusQueryHandler(IStatusRepository statusRepository)
        {
            _statusRepository= statusRepository;
        }
        public async Task<StatusDTO[]> Handle(GetStatusQuery request, CancellationToken cancellationToken)
        {
           return await _statusRepository.GetAllStatusAsync(cancellationToken);
        }
    }
}
