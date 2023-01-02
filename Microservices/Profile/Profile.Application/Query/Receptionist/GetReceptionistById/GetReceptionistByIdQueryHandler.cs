using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Receptionist.GetReceptionistById
{
    internal class GetReceptionistByIdQueryHandler : IRequestHandler<GetReceptionistByIdQuery, ReceptionistOneDTO>
    {
        private readonly IReceptionistRepository _receptionistRepository;

        public GetReceptionistByIdQueryHandler(IReceptionistRepository receptionistRepository)
        {
            _receptionistRepository = receptionistRepository;
        }
        public async  Task<ReceptionistOneDTO> Handle(GetReceptionistByIdQuery request, CancellationToken cancellationToken)
        {
           return await _receptionistRepository.GetReceptionistByIdAsync(request.ReceptionistId, cancellationToken);
        }
    }
}
