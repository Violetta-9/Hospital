using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.API.Application.Command.CreateService;
using Services.API.Application.Command.SetSpecializationForService;
using Services.API.Application.Command.UpdateService;
using Services.API.Application.Command.UpdateServiceStatus;
using Services.API.Application.Command.UpdateSpecializationForServices;
using Services.API.Application.Helpers;
using Services.API.Application.Query.GetAllServiceCategories;
using Services.API.Application.Query.GetAllServices;
using Services.API.Application.Query.GetEmptyServices;
using Services.API.Application.Query.GetServicesById;
using Services.API.Application.Query.GetServicesBySpecializationId;
using Services.API.Contracts.Incoming;
using Services.API.Contracts.Outgoing;
using Services.API.Controller.Abstraction.Mediator;
using Swashbuckle.AspNetCore.Annotations;

namespace Services.API.Controller.ServiceController;

[Route("api/[controller]")]
[Authorize(Roles = UserRoles.Receptionist)]
[ApiController]
public class ServiceController : MediatingControllerBase
{
    public ServiceController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("create")]
    [SwaggerOperation(Summary = "Create service", OperationId = "CreateService")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
    public async Task<ActionResult> CreateService([FromBody] CreateServiceDTO newServiceDto)
    {
        var query = new CreateServiceCommand(newServiceDto);
        return await SendRequestAsync(query);
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update service", OperationId = "UpdateService")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
    public async Task<ActionResult> UpdateService([FromBody] UpdateServiceDTO changeServiceDto)
    {
        var query = new UpdateServiceCommand(changeServiceDto);
        return await SendRequestAsync(query);
    }

    [HttpPatch]
    [SwaggerOperation(Summary = "Update service status", OperationId = "UpdateServiceStatus")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
    public async Task<ActionResult> UpdateServiceStatus([FromBody] UpdateServiceStatusDTO updateServiceStatusDto)
    {
        var query = new UpdateServiceStatusCommand(updateServiceStatusDto);
        return await SendRequestAsync(query);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all services", OperationId = "GetAllServices")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OutServicesDto[]))]
    public async Task<ActionResult> GetAllServices()
    {
        var query = new GetAllServicesQuery();
        return await SendRequestAsync(query);
    }

    [HttpGet("id/{id}")]
    [SwaggerOperation(Summary = "Get service by id", OperationId = "GetServiceById")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OutServicesDto))]
    public async Task<ActionResult> GetServiceById([FromRoute] long id)
    {
        var query = new GetServiceByIdQuery(id);
        return await SendRequestAsync(query);
    }

    [HttpPost("set")]
    [SwaggerOperation(Summary = "Set specialization for service", OperationId = "SetSpecializationForService")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
    public async Task<ActionResult> SetSpecializationForService([FromBody] SetSpecializationDTO setSpecializationDto)
    {
        var query = new SetSpecializationCommand(setSpecializationDto);
        return await SendRequestAsync(query);
    }

    [HttpPatch("update")]
    [SwaggerOperation(Summary = "Update specialization for service", OperationId = "UpdateSpecializationForService")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
    public async Task<ActionResult> UpdateSpecializationForService([FromBody] SetSpecializationDTO setSpecializationDto)
    {
        var query = new UpdateSpecializationForServiceCommand(setSpecializationDto.SpecializationId,setSpecializationDto.ServicesId);
        return await SendRequestAsync(query);
    }
    [HttpGet("categories")]
    [SwaggerOperation(Summary = "Get service categories", OperationId = "GetServiceCategories")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CategoriesDto[]))]
    public async Task<ActionResult> GetServiceCategories()
    {
        var query = new GetServiceCategoriesQuery();
        return await SendRequestAsync(query);
    }
    [HttpGet("service/free")]
    [SwaggerOperation(Summary = "Get empty services", OperationId = "GetEmptyServices")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(EmptyServices[]))]
    public async Task<ActionResult> GetEmptyServices()
    {
        var query = new GetEmptyServicesQuery();
        return await SendRequestAsync(query);
    }
    [AllowAnonymous]
    [HttpGet("service")]
    [SwaggerOperation(Summary = "Get services by specialization id", OperationId = "GetServicesBySpecializationId")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OutServicesDto[]))]
    public async Task<ActionResult> GetServiceBySpecializationId([FromQuery] long specializationId)
    {
        var query = new GetServicesBySpecializationIdQuery(specializationId);
        return await SendRequestAsync(query);
    }
}