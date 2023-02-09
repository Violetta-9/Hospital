using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Contracts.Outgoing;
using MediatR;

namespace Appointment.API.Application.Query.GetAppointmentForDoctor
{
    public class GetAppointmentForDoctorQuery : IRequest<AppointmentScheduleForDoctorDTO[]>
    {
        public DateTime Date { get; set; }
        public long DoctorId { get; set; }
        public GetAppointmentForDoctorQuery(long doctorId,DateTime date)
        {
            Date = date;
            DoctorId = doctorId;
        }
    }
}
