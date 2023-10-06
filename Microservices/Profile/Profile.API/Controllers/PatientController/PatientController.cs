using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile.API.Controllers.Abstraction.Mediator;
using Profile.Application.Command.Patients.AddPatientRole;
using Profile.Application.Command.Patients.DeletePatient;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;
using Profile.Application.Query.Patient.GetAllPatient;
using Profile.Application.Query.Patient.GetPatientByFullName;
using Profile.Application.Query.Patient.GetPatientById;
using Profile.Application.Query.Patient.GetPatientIdByAccountId;
using Swashbuckle.AspNetCore.Annotations;

namespace Profile.API.Controllers.PatientController;

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

    [HttpDelete]
    [SwaggerOperation(Summary = "Delete Patient", OperationId = "DeletePatient")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
    public async Task<ActionResult> DeletePatient([FromForm] string accountId)
    {
        var query = new DeletePatientCommand(accountId);
        return await SendRequestAsync(query);
    }

    [HttpPost("roles")]
    [SwaggerOperation(Summary = "Assign the Role To Patient", OperationId = "AssignPatientRole")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
    public async Task<ActionResult> AssignPatientRole([FromBody] string userId)
    {
        var query = new AddPatientRoleCommand(userId);
        return await SendRequestAsync(query);
    }
    [HttpGet("users")]
    [SwaggerOperation(Summary = "Find User", OperationId = "FindUser")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UsersDTO[]))]
    public async Task<ActionResult> FindUser([FromQuery] PatientsFullNameDTO userFullNameDto)
    {
        var query = new GetPatientByFullNameQuery(userFullNameDto);
        return await SendRequestAsync(query);
    }
    [HttpGet]
    [SwaggerOperation(Summary = "Get Patient Id By AccountId", OperationId = "GetPatientIdByAccountId")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
    public async Task<ActionResult> GetPatientIdByAccountId([FromQuery] string accountId)
    {
        var query = new GetPatientIdByAccountIdQuery(accountId);
        return await SendRequestAsync(query);
    }
}