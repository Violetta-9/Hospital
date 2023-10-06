using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Specialization.API.Application.Command.CreateSpecialization;
using Specialization.API.Application.Command.UpdateSpecialization;
using Specialization.API.Application.Command.UpdateSpecializationStatus;
using Specialization.API.Application.Contracts.Incoming;
using Specialization.API.Application.Contracts.Outgoing;
using Specialization.API.Application.Helpers;
using Specialization.API.Application.Query.GetAllSpecialization;
using Specialization.API.Application.Query.GetSpecializationById;
using Specialization.API.Controllers.Abstraction.Mediator;
using Swashbuckle.AspNetCore.Annotations;

namespace Specialization.API.Controllers;

[Authorize(Roles = UserRoles.Receptionist)]
[Route("api/[controller]")]
[ApiController]
public class SpecializationController : MediatingControllerBase
{
    public SpecializationController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("create")]
    [SwaggerOperation(Summary = "Create specialization", OperationId = "CreateSpecialization")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
    public async Task<ActionResult> CreateSpecialization([FromBody] CreateSpecializationDTO newSpecializationDto)
    {
        var query = new CreateSpecializationCommand(newSpecializationDto);
        return await SendRequestAsync(query);
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update specialization", OperationId = "UpdateSpecialization")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
    public async Task<ActionResult> UpdateSpecialization([FromBody] UpdateSpecializationDTO changeSpecializationDto)
    {
        var query = new UpdateSpecializationCommand(changeSpecializationDto);
        return await SendRequestAsync(query);
    }

    [HttpPatch]
    [SwaggerOperation(Summary = "Update specialization status", OperationId = "UpdateSpecializationStatus")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
    public async Task<ActionResult> UpdateSpecializationStatus(
        [FromBody] UpdateSpecializationStatusDTO updateSpecializationStatusDto)
    {
        var query = new UpdateSpecializationStatusCommand(updateSpecializationStatusDto);
        return await SendRequestAsync(query);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all specializations", OperationId = "GetAllSpecialization")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SpecializationListDTO[]))]
    public async Task<ActionResult> GetAllSpecialization()
    {
        var query = new GetAllSpecializationQuery();
        return await SendRequestAsync(query);
    }

    [HttpGet("id")]
    [SwaggerOperation(Summary = "Get specialization by id", OperationId = "GetSpecializationById")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SpecializationDTO))]
    public async Task<ActionResult> GetServiceById([FromQuery] long id)
    {
        var query = new GetSpecializationByIdQuery(id);
        return await SendRequestAsync(query);
    }
}