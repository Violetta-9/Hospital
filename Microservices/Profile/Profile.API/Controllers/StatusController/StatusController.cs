using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile.Application.Helpers;
using System.Data;
using Authorization.Data_Domain.Models;
using Profile.API.Controllers.Abstraction.Mediator;
using MediatR;
using Office.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Query.Patient.GetAllPatient;
using Profile.Application.Query.Status.GetStatus;
using Swashbuckle.AspNetCore.Annotations;

namespace Profile.API.Controllers.StatusController
{
    [Route("api/[controller]")]
    [Authorize(Roles = $"{UserRoles.Receptionist},{UserRoles.Doctor}")]
    [ApiController]
    public class StatusController : MediatingControllerBase
    {
        public StatusController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet()]
        [SwaggerOperation(Summary = "Get All Status", OperationId = "GetAllStatus")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StatusDTO[]))]
        public async Task<ActionResult> GetAllStatus()
        {
            var query = new GetStatusQuery();
            return await SendRequestAsync(query);
        }
    }
}
