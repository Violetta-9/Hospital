﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Services.API.Controller.Abstraction.Mediator;
using MediatR;
using Services.API.Contracts.Incoming;
using Services.API.Application.Command.CreateService;
using Swashbuckle.AspNetCore.Annotations;
using Office.Application.Contracts.Incoming;
using Services.API.Application.Command.UpdateService;
using Services.API.Application.Command.UpdateServiceStatus;
using Services.API.Application.Query.GetAllServices;
using Services.API.Application.Query.GetServicesById;
using Services.API.Contracts.Outgoing;

namespace Services.API.Controller.ServiceController
{
    [Route("api/[controller]")]
    //[Authorize(Roles = UserRoles.Receptionist)]
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
        [HttpGet("id")]
        [SwaggerOperation(Summary = "Get service by id", OperationId = "GetServiceById")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(OutServicesDto))]
        public async Task<ActionResult> GetServiceById([FromQuery] long id)
        {
            var query = new GetServiceByIdQuery(id);
            return await SendRequestAsync(query);
        }
    }
}
