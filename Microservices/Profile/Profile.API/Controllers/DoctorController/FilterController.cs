using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile.API.Controllers.Abstraction.Mediator;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;
using Profile.Application.Query.Doctor.DoctorFilter;
using Profile.Application.Query.Doctor.GetDoctorBySpesializationId;
using Swashbuckle.AspNetCore.Annotations;

namespace Profile.API.Controllers.DoctorController
{
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoles.Receptionist)]
    [ApiController]
    public class FilterController : MediatingControllerBase
    {
        public FilterController(IMediator mediator) : base(mediator)
        {
        }

        
        [HttpGet]
        [SwaggerOperation(Summary = "Doctor Filter", OperationId = "DoctorFilter")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DoctorAllDTO[]))]
        public async Task<ActionResult> DoctorFilter([FromQuery]DoctorFilterDTO doctorFilterDto)
        {
            var query = new DoctorFilterQuery(doctorFilterDto);
            return await SendRequestAsync(query);
        }
    }
}
