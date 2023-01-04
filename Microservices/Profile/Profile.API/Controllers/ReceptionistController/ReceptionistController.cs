using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile.API.Controllers.Abstraction.Mediator;

using Profile.Application.Contracts.Incoming;
using Profile.Application.Helpers;
using Swashbuckle.AspNetCore.Annotations;
using Profile.Application.Command.Receptionists.AddReceptionistRole;

using Profile.Application.Command.Receptionists.UpdateOffice;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Command.Receptionists.DeleteReceptionist;
using Profile.Application.Query.Receptionist.GetAllReceptionist;
using Profile.Application.Query.Receptionist.GetReceptionistById;

namespace Profile.API.Controllers.ReceptionistController
{
    [Route("api/[controller]")]
    [Authorize(Roles =UserRoles.Receptionist )]
    
    [ApiController]
    public class ReceptionistController : MediatingControllerBase
    {
        public ReceptionistController(IMediator mediator) : base(mediator)
        {

        }
        [HttpPost("roles")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerOperation(Summary = "Assign receptionist role", OperationId = "AssignReceptionistRole")]
        public async Task<ActionResult> AssignReceptionistRole([FromBody] ReceptionistDTO receptionistDto)
        {
            var query = new AddReceptionistRoleCommand(receptionistDto);
            return await SendRequestAsync(query);
        }
        [HttpDelete]
        [SwaggerOperation(Summary = "Delete Receptionist", OperationId = "DeleteReceptionist")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> DeleteReceptionist([FromForm] string accountId)
        {
            var query = new DeleteReceptionistCommand(accountId);
            return await SendRequestAsync(query);
        }
        [HttpPatch]
        [SwaggerOperation(Summary = "Update Office", OperationId = "UpdateOffice")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<ActionResult> UpdateOffice([FromForm] UpdateReceptionistDTO receptionistDto)
        {
            var query = new UpdateOfficeCommand(receptionistDto);
            return await SendRequestAsync(query);
        }
        [HttpGet("all")]
        [SwaggerOperation(Summary = "Get All Receptionists", OperationId = "GetAllReceptionists")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ReceptionistAllDTO[]))]
        public async Task<ActionResult> GetAllReceptionists()
        {
            var query = new GetAllReceptionistQuery();
            return await SendRequestAsync(query);
        }
        [HttpGet("{receptionistId}")]
        [SwaggerOperation(Summary = "Get Receptionist By Id", OperationId = "GetReceptionistById")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ReceptionistOneDTO))]
        public async Task<ActionResult> GetReceptionistById([FromRoute] long receptionistId)
        {
            var query = new GetReceptionistByIdQuery(receptionistId);
            return await SendRequestAsync(query);
        }

    }
}
