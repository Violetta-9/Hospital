using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Contracts.Outgoing;
using MediatR;

namespace Appointment.API.Application.Query.GetAppointmentByPatientId
{
    public class GetAppointmentByPatientIdQuery : IRequest<AppointmentHistoryDTO[]>
    {
        public long PatientId { get; set; }

        public GetAppointmentByPatientIdQuery(long patientId)
        {
            PatientId=patientId;
        }
    }
}
