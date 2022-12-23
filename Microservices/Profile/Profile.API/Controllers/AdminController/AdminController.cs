using Profile.API.Controllers.Abstraction.Mediator;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Profile.Application.Command.Admin;
using Swashbuckle.AspNetCore.Annotations;

namespace Profile.API.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : MediatingControllerBase
    {
        public AdminController(IMediator mediator) : base(mediator)
        {
            
        }
        [HttpPost("AddReceptionistRole")]
        [SwaggerOperation(Summary = "Add receptionist role", OperationId = "AddReceptionistRole")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<ActionResult> AddReceptionistRole([FromBody] string userId)
        {
            var query = new AddReceptionistRoleCommand(userId);
           return await SendRequestAsync(query);
        }


    }
}
