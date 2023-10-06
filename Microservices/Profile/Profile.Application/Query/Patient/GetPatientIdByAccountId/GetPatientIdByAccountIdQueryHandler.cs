using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;

namespace Profile.Application.Query.Patient.GetPatientIdByAccountId
{
    internal class GetPatientIdByAccountIdQueryHandler : IRequestHandler<GetPatientIdByAccountIdQuery, long>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientIdByAccountIdQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository=patientRepository;
        }
        public async Task<long> Handle(GetPatientIdByAccountIdQuery request, CancellationToken cancellationToken)
        {
            return await _patientRepository.GetPatientIdByAccountId(request.AccountId, cancellationToken);
        }
    }
}
