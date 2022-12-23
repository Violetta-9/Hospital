using MediatR;
using Microsoft.AspNetCore.Mvc;
using Profile.API.Controllers.Abstraction.Mediator;
using Profile.Application.Command.Admin;
using Profile.Application.Command.Receptionist.AddDoctorRole;
using Profile.Application.Command.Receptionist.AddPatientRole;
using Swashbuckle.AspNetCore.Annotations;

namespace Profile.API.Controllers.ReseptionistController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionistController : MediatingControllerBase
    {
        public ReceptionistController(IMediator mediator) : base(mediator)
        {

        }
        [HttpPost("AddDoctorRole")]
        [SwaggerOperation(Summary = "Add patient role", OperationId = "AddDoctorRole")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<ActionResult> AddDoctorRole([FromBody] string userId)
        {
            var query = new AddDoctorRoleCommand(userId);
            return await SendRequestAsync(query);
        }
        [HttpPost("AddPatientRole")]
        [SwaggerOperation(Summary = "Add patient role", OperationId = "AddPatientRole")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<ActionResult> AddPatientRole([FromBody] string userId)
        {
            var query = new AddPatientRoleCommand(userId);
            return await SendRequestAsync(query);
        }
    }
}
