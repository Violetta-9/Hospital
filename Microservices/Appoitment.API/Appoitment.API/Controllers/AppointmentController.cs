using Appoitment.API.Controllers.Abstraction.Mediator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using Appointment.API.Application.Command.Appointment.ApproveAppointment;
using Appointment.API.Application.Command.Appointment.CancelAppointment;
using Appointment.API.Application.Command.Appointment.CreateAppointment;
using Appointment.API.Application.Command.Appointment.RescheduleAppointment;
using Appointment.API.Application.Contracts.Incoming;
using Appointment.API.Application.Contracts.Outgoing;
using Appointment.API.Application.Query.GetAppointmentByPatientId;
using Appointment.API.Application.Query.GetAppointmentForDoctor;
using Appointment.API.Application.Query.GetAppointmentForReceptionist;
using Appointment.API.Application.Helpers;

namespace Appoitment.API.Controllers
{
    [Route("api/[controller]")]
 
    [ApiController]
    public class AppointmentController : MediatingControllerBase
    {
        public AppointmentController(IMediator mediator) : base(mediator)
        {
        }
        [Authorize]
        [HttpPost]
        [SwaggerOperation(Summary = "Create Appointment", OperationId = "CreateAppointment")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
        public async Task<ActionResult> CreateAppointment([FromForm] CreateAppointmentDTO appointmentDto)
        {
            var query = new CreateAppointmentCommand(appointmentDto);
            return await SendRequestAsync(query);
        }
        [Authorize]
        [HttpPatch]
        [SwaggerOperation(Summary = "Reschedule Appointment", OperationId = "RescheduleAppointment")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> RescheduleAppointment([FromForm] RescheduleAppointmentDTO appointmentDto)
        {
            var query = new RescheduleAppointmentCommand(appointmentDto);
            return await SendRequestAsync(query);
        }
        [Authorize(Roles = UserRoles.Receptionist)]
        [HttpPatch("approve")]
        [SwaggerOperation(Summary = "Approve Appointment", OperationId = "ApproveAppointment")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> ApproveAppointment([FromForm]  long appointmentId,[FromForm] bool isApprove)
        {
            var query = new ApproveAppointmentCommand(appointmentId,isApprove);
            return await SendRequestAsync(query);
        }
        [Authorize(Roles = UserRoles.Receptionist)]
        [HttpDelete]
        [SwaggerOperation(Summary = "Cancel Appointment", OperationId = "CancelAppointment")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> CancelAppointment([FromForm] long appointmentId)
        {
            var query = new CancelAppointmentCommand(appointmentId);
            return await SendRequestAsync(query);
        }
        [Authorize(Roles = UserRoles.Doctor)]
        [HttpGet("doctor")]
        [SwaggerOperation(Summary = "Get Appointment List For Doctor", OperationId = "GetAppointmentListForDoctor")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AppointmentScheduleForDoctorDTO[]))]
        public async Task<ActionResult> GetAppointmentListForDoctor([FromQuery] long doctorId, [FromQuery] DateTime dataTime)
        {
            var query = new GetAppointmentForDoctorQuery(doctorId,dataTime);
            return await SendRequestAsync(query);
        }
        [Authorize(Roles = UserRoles.Receptionist)]
        [HttpGet("receptionist")]
        [SwaggerOperation(Summary = "Get Appointment List For Receptionist", OperationId = "GetAppointmentListForReceptionist")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AppointmentScheduleForReceptionistDTO[]))]
        public async Task<ActionResult> GetAppointmentListForReceptionist([FromQuery] long officeId, [FromQuery] DateTime dataTime)
        {
            var query = new GetAppointmentForReceptionistQuery(dataTime,officeId);
            return await SendRequestAsync(query);
        }
        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("patient")]
        [SwaggerOperation(Summary = "Get Appointment List For Patient", OperationId = "GetAppointmentListForPatient")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AppointmentHistoryDTO[]))]
        public async Task<ActionResult> GetAppointmentListForPatient([FromQuery] long patientId)
        {
            var query = new GetAppointmentByPatientIdQuery(patientId);
            return await SendRequestAsync(query);
        }
    }
}
