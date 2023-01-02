using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using MediatR.Pipeline;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Patient.GetPatientById
{
    internal class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientOneDTO>
    {
        private readonly IPatientRepository _patientRepository;
        public GetPatientByIdQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientOneDTO> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            return await _patientRepository.GetPatientByIdAsync(request.PatientId, cancellationToken);
        }
    }
}
