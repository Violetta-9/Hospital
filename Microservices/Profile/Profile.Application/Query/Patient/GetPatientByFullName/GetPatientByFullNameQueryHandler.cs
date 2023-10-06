using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.API.Client.GeneratedClient;
using Authorization.Data.Repository;
using MediatR;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Query.Doctor.GetDoctorByFullName;

namespace Profile.Application.Query.Patient.GetPatientByFullName
{
    public class GetPatientByFullNameQueryHandler : IRequestHandler<GetPatientByFullNameQuery, UsersDTO[]>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientByFullNameQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

       
        public async Task<UsersDTO[]> Handle(GetPatientByFullNameQuery request, CancellationToken cancellationToken)
        {
          return await _patientRepository.FindUsersByFullName(request.PatientsFullName.FirstName,
                request.PatientsFullName.LastName, request.PatientsFullName.MiddleName, cancellationToken);
        }
    }
}
