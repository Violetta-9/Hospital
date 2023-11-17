using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile.API.Controllers.Abstraction.Mediator;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;
using Profile.Application.Query.Doctor.GetAllDoctors;
using Profile.Application.Query.Status;
using Swashbuckle.AspNetCore.Annotations;

namespace Profile.API.Controllers.StatusController
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class StatusController : MediatingControllerBase
    {
        public StatusController(IMediator mediator) : base(mediator)
        {
        }
       
        [HttpGet]
        [SwaggerOperation(Summary = "Get All Status", OperationId = "GetAllStatus")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StatusAllDto[]))]
        public async Task<ActionResult> GetAllStatus()
        {
            var query = new GetAllStatusQuery();
            return await SendRequestAsync(query);
        }
    }
}
