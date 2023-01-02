using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Patient.GetAllPatient
{
    internal class GetAllPatientQueryHandler : IRequestHandler<GetAllPatientQuery, PatientAllDTO[]>
    {
        private readonly IPatientRepository _patientRepository;

        public GetAllPatientQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository=patientRepository;
        }
        public async Task<PatientAllDTO[]> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
        {
            return await _patientRepository.GetAllPatientsAsync(cancellationToken);
        }
    }
}
