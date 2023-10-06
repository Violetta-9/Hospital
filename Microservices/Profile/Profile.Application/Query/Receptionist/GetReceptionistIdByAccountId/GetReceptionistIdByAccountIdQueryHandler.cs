using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;

namespace Profile.Application.Query.Receptionist.GetReceptionistIdByAccountId
{
    internal class GetReceptionistIdByAccountIdQueryHandler : IRequestHandler<GetReceptionistIdByAccountIdQuery, long>
    {
        private readonly IReceptionistRepository _receptionistRepository;

        public GetReceptionistIdByAccountIdQueryHandler(IReceptionistRepository receptionistRepository)
        {
            _receptionistRepository=receptionistRepository;
        }
        public async Task<long> Handle(GetReceptionistIdByAccountIdQuery request, CancellationToken cancellationToken)
        {
            return await _receptionistRepository.GetReceptionistIdByAccountIdAsync(request.AccountId,
                cancellationToken);
        }
    }
}
