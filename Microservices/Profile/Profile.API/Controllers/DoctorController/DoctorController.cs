using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile.Application.Helpers;
using System.Data;
using Profile.API.Controllers.Abstraction.Mediator;
using MediatR;
using Profile.Application.Command.Receptionists.UpdateDoctor.UpdateDoctorProfile;
using Profile.Application.Command.Receptionists.UpdateDoctor.UpdateDoctorStatus;

using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;
using Swashbuckle.AspNetCore.Annotations;

namespace Profile.API.Controllers.DoctorController
{
    [ApiController]
    [Authorize(Roles = $"{UserRoles.Doctor},{UserRoles.Receptionist},{UserRoles.Admin}")]
    public class DoctorController : MediatingControllerBase
    {
        public DoctorController(IMediator mediator) : base(mediator)
        {
        }
      
        [HttpPost("UpdateStatus")]
        [SwaggerOperation(Summary = "Update Status", OperationId = "UpdateStatus")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> UpdateStatus([FromForm] bool status, [FromForm] string accountId)
        {
            var query = new UpdateDoctorStatusCommand(status,accountId);
            return await SendRequestAsync(query);
        }
        [HttpPost("UpdateDoctorProfile")]
        [SwaggerOperation(Summary = "Update Doctor Profile", OperationId = "UpdateDoctorProfile")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> UpdateDoctorProfile([FromBody] UpdateDoctorDTO updateDoctorDTO)
        {
            var query = new UpdateDoctorProfileCommand(updateDoctorDTO);
            return await SendRequestAsync(query);
        }
    }
}
