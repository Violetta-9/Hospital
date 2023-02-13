using Appointment.API.Application.Command.Appointment.CreateAppointment;
using Appointment.API.Application.Command.AppointmentResult.CreateAppointmentResult;
using Appointment.API.Application.Contracts.Incoming;
using Appoitment.API.Controllers.Abstraction.Mediator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Appoitment.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AppointmentResultController : MediatingControllerBase
    {
        public AppointmentResultController(IMediator mediator) : base(mediator)
        {
        }

       
        [HttpPost]
        [SwaggerOperation(Summary = "Create Appointment Result", OperationId = "CreateAppointmentResult")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
        public async Task<ActionResult> CreateAppointmentResult([FromForm] CreateAppointmentResultDTO resultDto)
        {
            var query = new CreateAppointmentResultCommand(resultDto);
            return await SendRequestAsync(query);
        }
    }
}
