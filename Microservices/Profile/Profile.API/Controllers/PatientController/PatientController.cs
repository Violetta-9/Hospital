using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile.Application.Helpers;
using Profile.API.Controllers.Abstraction.Mediator;
using MediatR;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Query.Patient.GetAllPatient;
using Profile.Application.Query.Patient.GetPatientById;
using Swashbuckle.AspNetCore.Annotations;

namespace Profile.API.Controllers.PatientController
{
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoles.Receptionist)]
    [ApiController]
    public class PatientController : MediatingControllerBase
    {
        public PatientController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet("all")]
        [SwaggerOperation(Summary = "Get All Patients", OperationId = "GetAllPatients")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PatientAllDTO[]))]
        public async Task<ActionResult> GetAllPatients()
        {
            var query = new GetAllPatientQuery();
            return await SendRequestAsync(query);
        }
        [HttpGet("{patientId}")]
        [SwaggerOperation(Summary = "Get Patient By Id", OperationId = "GetPatientById")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PatientOneDTO))]
        public async Task<ActionResult> GetPatientById([FromRoute] long patientId)
        {
            var query = new GetPatientByIdQuery(patientId);
            return await SendRequestAsync(query);
        }
    }
}
