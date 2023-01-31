using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile.API.Controllers.Abstraction.Mediator;
using Profile.Application.Command.Photo.AddPhoto;
using Profile.Application.Command.Photo.DeletePhoto;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace Profile.API.Controllers.PhotoController;

[Route("api/[controller]")]
[Authorize(Roles = UserRoles.Receptionist)]
[ApiController]
public class PhotoController : MediatingControllerBase
{
    public PhotoController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Add photo", OperationId = "AddPhoto")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
    public async Task<ActionResult> AddPhoto([FromForm] AddPhotoDTO photoDto)
    {
        var query = new AddPhotoCommand(photoDto);
        return await SendRequestAsync(query);
    }

    [HttpDelete]
    [SwaggerOperation(Summary = "Delete photo ", OperationId = "DeletePhoto")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
    public async Task<ActionResult> DeletePhoto([FromBody] DeletePhotoDTO photoDto)
    {
        var query = new DeletePhotoCommand(photoDto);
        return await SendRequestAsync(query);
    }
}