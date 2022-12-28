using Profile.API.Controllers.Abstraction.Mediator;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Profile.Application.Command.Admin;
using Profile.Application.Contracts.Incoming;
using Microsoft.AspNetCore.Authorization;
using Profile.Application.Helpers;
using Swashbuckle.AspNetCore.Annotations;
using Profile.Application.Command.Receptionists.DeletePatient;
using Profile.Application.Command.Receptionists.DeleteReceptionist;
using Profile.Application.Contracts.Outgoing;

namespace Profile.API.Controllers.AdminController
{
    [ApiController]
    [Authorize(Roles =UserRoles.Admin)]
    public class AdminController : MediatingControllerBase
    {
        public AdminController(IMediator mediator) : base(mediator)
        {
            
        }
        [HttpPost("AddReceptionistRole")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerOperation(Summary = "Add receptionist role", OperationId = "AddReceptionistRole")]
        public async Task<ActionResult> AddReceptionistRole([FromBody] ReceptionistDTO receptionistDto)
        {
            var query = new AddReceptionistRoleCommand(receptionistDto);
           return await SendRequestAsync(query);
        }
        [HttpDelete("DeleteReceptionist")]
        [SwaggerOperation(Summary = "Delete Receptionist", OperationId = "DeleteReceptionist")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> DeletePatient([FromForm] string accountId)
        {
            var query = new DeleteReceptionistCommand(accountId);
            return await SendRequestAsync(query);
        }


    }
}
