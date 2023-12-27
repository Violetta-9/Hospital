
using Appointment.API.Application.Contracts.Outgoing;
using MediatR;

namespace Appointment.API.Application.Query.GetBusySlots
{
    public class GetBusySlotsQuery : IRequest<BusyTimeSlotsDto[]>
    {
        public long DoctorId { get; set; }
        public DateTime DateTime { get; set; }

        public GetBusySlotsQuery(long doctorId, DateTime dateTime)
        {
            DoctorId = doctorId;
            DateTime = dateTime;
        }
    }
}
