using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Office.Application.Command.CreateOffice;
using Office.Application.Command.UpdateOffice;
using Office.Application.Command.UpdateOfficeStatus;
using Office.Application.Contracts.Incoming;
using Office.Application.Contracts.Outgoing;
using Office.Application.Helpers;
using Office.Application.Query.GetAllOffices;
using Office.Application.Query.GetOfficeById;
using Office.Controllers.Abstraction.Mediator;
using Swashbuckle.AspNetCore.Annotations;

namespace Office.Controllers;

[Authorize(Roles = UserRoles.Receptionist)]
[Route("api/[controller]")]
[ApiController]
public class OfficeController : MediatingControllerBase
{
    public OfficeController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("create")]
    [SwaggerOperation(Summary = "Create office", OperationId = "CreateOffice")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
    public async Task<ActionResult> CreateOffice([FromForm] CreateOfficeDTO newOfficeDto)
    {
        var query = new CreateOfficeCommand(newOfficeDto);
        return await SendRequestAsync(query);
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update office", OperationId = "UpdateOffice")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
    public async Task<ActionResult> UpdateOffice([FromForm] UpdateOfficeDTO changeOfficeDto)
    {
        var query = new UpdateOfficeCommand(changeOfficeDto);
        return await SendRequestAsync(query);
    }

    [HttpPatch]
    [SwaggerOperation(Summary = "Update office status", OperationId = "UpdateOfficeStatus")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
    public async Task<ActionResult> UpdateOfficeStatus([FromBody] UpdateOfficeStatusDTO updateOfficeStatus)
    {
        var query = new UpdateOfficeStatusCommand(updateOfficeStatus);
        return await SendRequestAsync(query);
    }
    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation(Summary = "Get all offices", OperationId = "GetAllOffices")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AllOfficesDto[]))]
    public async Task<ActionResult> GetAllOffices()
    {
        var query = new GetAllOfficesQuery();
        return await SendRequestAsync(query);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get office by id offices", OperationId = "GetOfficeById")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OfficeDto))]
    public async Task<ActionResult> GetOfficeById([FromRoute] long id)
    {
        var query = new GetOfficeByIdQuery(id);
        return await SendRequestAsync(query);
    }
}