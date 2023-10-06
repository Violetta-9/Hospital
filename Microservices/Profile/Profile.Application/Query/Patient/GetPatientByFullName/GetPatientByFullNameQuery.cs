using Profile.Application.Contracts.Incoming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.API.Client.GeneratedClient;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Patient.GetPatientByFullName
{
    public class GetPatientByFullNameQuery :  IRequest<UsersDTO[]>
    {
        public PatientsFullNameDTO PatientsFullName { get; set; }

        public GetPatientByFullNameQuery(PatientsFullNameDTO patients)
        {
            PatientsFullName = patients;
        }
    }
}
