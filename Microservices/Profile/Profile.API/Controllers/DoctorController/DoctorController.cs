using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile.API.Controllers.Abstraction.Mediator;
using Profile.Application.Command.Doctors.AddDoctorRole;
using Profile.Application.Command.Doctors.DeleteDoctor;
using Profile.Application.Command.Doctors.UpdateDoctor.UpdateDoctorProfile;
using Profile.Application.Command.Doctors.UpdateDoctor.UpdateDoctorStatus;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;
using Profile.Application.Query.Doctor.GetAllDoctors;
using Profile.Application.Query.Doctor.GetDoctorByFullName;
using Profile.Application.Query.Doctor.GetDoctorById;
using Profile.Application.Query.Doctor.GetDoctorByOfficeId;
using Profile.Application.Query.Doctor.GetDoctorBySpesializationId;
using Profile.Application.Query.Doctor.GetDoctorIdByAccountId;
using Profile.Application.Query.Receptionist.GetReceptionistIdByAccountId;
using Swashbuckle.AspNetCore.Annotations;

namespace Profile.API.Controllers.DoctorController;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class DoctorController : MediatingControllerBase
{
    public DoctorController(IMediator mediator) : base(mediator)
    {
    }
    [Authorize(Roles = UserRoles.Receptionist)]
    [HttpPost("roles")]
    [SwaggerOperation(Summary = "Assign the Role To Doctor", OperationId = "AssignRoleToDoctor")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
    public async Task<ActionResult> AssignRoleToDoctor([FromForm] DoctorDTO doctor)
    {
        var query = new AddDoctorRoleCommand(doctor);
        return await SendRequestAsync(query);
    }
    [Authorize(Roles = UserRoles.Receptionist)]
    [HttpDelete]
    [SwaggerOperation(Summary = "Delete Doctor", OperationId = "DeleteDoctor")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
    public async Task<ActionResult> DeleteDoctor([FromBody] string accountId)
    {
        var query = new DeleteDoctorCommand(accountId);
        return await SendRequestAsync(query);
    }
    [Authorize(Roles =$"{UserRoles.Receptionist},{UserRoles.Doctor}")]
    [HttpPatch("status")]
    [SwaggerOperation(Summary = "Update Status", OperationId = "UpdateStatus")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
    public async Task<ActionResult> UpdateStatus([FromForm] long status, [FromForm] string accountId)
    {
        var query = new UpdateDoctorStatusCommand(status, accountId);
        return await SendRequestAsync(query);
    }
    [Authorize(Roles = $"{UserRoles.Receptionist},{UserRoles.Doctor}")]
    [HttpPatch("update")]
    [SwaggerOperation(Summary = "Update Doctor Profile", OperationId = "UpdateDoctorProfile")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
    public async Task<ActionResult> UpdateDoctorProfile([FromBody] UpdateDoctorDTO updateDoctorDTO)
    {
        var query = new UpdateDoctorProfileCommand(updateDoctorDTO);
        return await SendRequestAsync(query);
    }
    [Authorize(Roles = UserRoles.Receptionist)]
    [HttpGet("all")]
    [SwaggerOperation(Summary = "Get All Doctors", OperationId = "GetAllDoctors")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DoctorAllDTO[]))]
    public async Task<ActionResult> GetAllDoctors()
    {
        var query = new GetAllDoctorsQuery();
        return await SendRequestAsync(query);
    }
    [Authorize(Roles = $"{UserRoles.Receptionist},{UserRoles.Doctor}")]
    [HttpGet("{doctorId}")]
    [SwaggerOperation(Summary = "Get Doctor By Id", OperationId = "GetDoctorById")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DoctorOneDTO))]
    public async Task<ActionResult> GetDoctorById([FromRoute] long doctorId)
    {
        var query = new GetDoctorByIdQuery(doctorId);
        return await SendRequestAsync(query);
    }
    [Authorize(Roles = UserRoles.Receptionist)]
    [HttpGet("offices/{officeId}")]
    [SwaggerOperation(Summary = "Get Doctor By Id", OperationId = "GetDoctorsByOfficeId")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DoctorAllDTO[]))]
    public async Task<ActionResult> GetDoctorsByOfficeId([FromRoute] long officeId)
    {
        var query = new GetDoctorsByOfficeIdQuery(officeId);
        return await SendRequestAsync(query);
    }
    [AllowAnonymous]
    [HttpGet("specialization")]
    [SwaggerOperation(Summary = "Get Doctors By Specialization Id", OperationId = "GetDoctorsBySpecializationId")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DoctorAllDTO[]))]
    public async Task<ActionResult> GetDoctorsBySpecializationId([FromQuery] long specializationId)
    {
        var query = new GetDoctorsBySpesializationIdQuery(specializationId);
        return await SendRequestAsync(query);
    }
    [Authorize(Roles = UserRoles.Receptionist)]
    [HttpGet("fullname")]
    [SwaggerOperation(Summary = "Find Doctor By FullName", OperationId = "FindDoctorByFullName")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DoctorAllDTO[]))]
    public async Task<ActionResult> FindDoctorByFullName([FromQuery] DoctorsFullNameDTO doctorsFullName)
    {
        var query = new GetDoctorByFullNameQuery(doctorsFullName);
        return await SendRequestAsync(query);
    }
    [Authorize(Roles =UserRoles.Doctor)]
    [HttpGet]
    [SwaggerOperation(Summary = "Get Doctor Id By AccountId", OperationId = "GetDoctorIdByAccountId")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
    public async Task<ActionResult> GetDoctorIdByAccountId([FromQuery] string accountId)
    {
        var query = new GetDoctorIdByAccountIdQuery(accountId);
        return await SendRequestAsync(query);
    }
}