using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile.Application.Helpers;
using System.Data;
using Profile.API.Controllers.Abstraction.Mediator;
using MediatR;

using Profile.Application.Command.Update.UpdateAccountIfo;
using Profile.Application.Command.Update.UpdatePassword;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;
using Swashbuckle.AspNetCore.Annotations;

namespace Profile.API.Controllers.UpdateProfile
{
    [ApiController]
    [Authorize]
    public class UpdateController : MediatingControllerBase
    {
        public UpdateController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPut("UpdateProfile")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(Summary = "Update Profile", OperationId = "UpdateProfile")]
        public async Task<ActionResult> UpdateProfile([FromBody] UpdateAccountInfoDTO updateUser)
        {
            var query = new UpdateAccountInfoCommand(updateUser) ;
            return await SendRequestAsync(query);
        }
        [HttpPatch("UpdatePassword")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        [SwaggerOperation(Summary = "Update Password", OperationId = "UpdatePassword")]
        public async Task<ActionResult> UpdatePassword([FromBody] UpdatePasswordDTO updatePassword)
        {
            var query = new UpdatePasswordCommand(updatePassword) ;
            return await SendRequestAsync(query);
        }

    }
}
