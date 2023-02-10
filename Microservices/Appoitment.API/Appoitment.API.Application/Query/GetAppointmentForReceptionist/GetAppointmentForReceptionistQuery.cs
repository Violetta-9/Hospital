using Appointment.API.Application.Contracts.Outgoing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.API.Application.Query.GetAppointmentForReceptionist
{
    public class GetAppointmentForReceptionistQuery : IRequest<AppointmentScheduleForReceptionistDTO[]>
    {
        public DateTime Date { get; set; }
        public long OfficeId { get; set; }
        public GetAppointmentForReceptionistQuery(DateTime date, long officeId)
        {
            Date = date;
            OfficeId = officeId;
        }
    }
  
}
