using System.ComponentModel.DataAnnotations;
using Authorization.Application.Command.User.ConfirmEmail;
using Authorization.Application.Command.User.Registration;
using Authorization.Application.Contracts.Incoming.User;
using Authorization.Application.Contracts.Outgoing;
using Authorization.Application.Query.User;
using Authorization_API.Controllers.Abstraction.Mediator;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Authorization.API.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class UserController : MediatingControllerBase
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("registration")]
    [SwaggerOperation(Summary = "Register new user", OperationId = "RegisterUser")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AuthorizationId))]
    public async Task<ActionResult> Registration([FromBody] UserDTO newUser)
    {
        var query = new RegistrationCommand(newUser);
        return await SendRequestAsync(query);
    }

    [HttpPost("login")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
    [SwaggerOperation(Summary = "Login", OperationId = "Login")]
    public async Task<ActionResult> Login([FromForm] LoginDTO login)
    {
        var query = new LoginQuery(login);
        return await SendRequestAsync(query);
    }

    [HttpGet("confirmation/{userId}")]
   
    public async Task<IActionResult> ConfirmEmail([FromRoute] [Required] string userId,
        [FromQuery] [Required] string token)
    {
        var query = new ConfirmEmailCommand(userId, token);
        return await SendRequestAsync(query);
    }
}