using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile.API.Controllers.Abstraction.Mediator;
using Profile.Application.Command.Receptionists.UpdateDoctor.UpdateDoctorProfile;
using Profile.Application.Command.Receptionists.UpdateDoctor.UpdateDoctorStatus;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using Profile.Application.Command.Doctors.DeleteDoctor;
using Profile.Application.Query.Doctor.GetAllDoctors;
using Profile.Application.Command.Receptionists.AddDoctorRole;
using Profile.Application.Command.Receptionists.DeleteDoctor;
using Profile.Application.Query.Doctor.GetDoctorByFullName;
using Profile.Application.Query.Doctor.GetDoctorById;
using Profile.Application.Query.Doctor.GetDoctorByOfficeId;
using Profile.Application.Query.Doctor.GetDoctorBySpesializationId;

namespace Profile.API.Controllers.DoctorController
{

    [Route("api/[controller]")]
    [Authorize(Roles = UserRoles.Receptionist)]
    [ApiController]
    public class DoctorController : MediatingControllerBase
    {
        public DoctorController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost("roles")]
        [SwaggerOperation(Summary = "Assign the Role To Doctor", OperationId = "AssignRoleToDoctor")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> AssignRoleToDoctor([FromBody] DoctorDTO doctor)
        {
            var query = new AddDoctorRoleCommand(doctor);
            return await SendRequestAsync(query);
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete Doctor", OperationId = "DeleteDoctor")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> DeleteDoctor([FromBody] string accountId)
        {
            var query = new DeleteDoctorCommand(accountId);
            return await SendRequestAsync(query);
        }

        [HttpPatch("status")]
        [SwaggerOperation(Summary = "Update Status", OperationId = "UpdateStatus")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> UpdateStatus([FromForm] long status, [FromForm] string accountId)
        {
            var query = new UpdateDoctorStatusCommand(status, accountId);
            return await SendRequestAsync(query);
        }
        [HttpPatch("update")]
        [SwaggerOperation(Summary = "Update Doctor Profile", OperationId = "UpdateDoctorProfile")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> UpdateDoctorProfile([FromBody] UpdateDoctorDTO updateDoctorDTO)
        {
            var query = new UpdateDoctorProfileCommand(updateDoctorDTO);
            return await SendRequestAsync(query);
        }

        [HttpGet("all")]
        [SwaggerOperation(Summary = "Get All Doctors", OperationId = "GetAllDoctors")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DoctorAllDTO[]))]
        public async Task<ActionResult> GetAllDoctors()
        {
            var query = new GetAllDoctorsQuery();
            return await SendRequestAsync(query);
        }
        [HttpGet("{doctorId}")]
        [SwaggerOperation(Summary = "Get Doctor By Id", OperationId = "GetDoctorById")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DoctorOneDTO))]
        public async Task<ActionResult> GetDoctorById([FromRoute] long doctorId)
        {
            var query = new GetDoctorByIdQuery(doctorId);
            return await SendRequestAsync(query);
        }
        [HttpGet("offices/{officeId}")]
        [SwaggerOperation(Summary = "Get Doctor By Id", OperationId = "GetDoctorsByOfficeId")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DoctorAllDTO[]))]
        public async Task<ActionResult> GetDoctorsByOfficeId([FromRoute] long officeId)
        {
            var query = new GetDoctorsByOfficeIdQuery(officeId);
            return await SendRequestAsync(query);
        }

        [HttpGet("specialization/{id}")]
        [SwaggerOperation(Summary = "Get Doctors By Specialization Id", OperationId = "GetDoctorsBySpecializationId")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DoctorAllDTO[]))]
        public async Task<ActionResult> GetDoctorsBySpecializationId([FromQuery] long specializationId)
        {
            var query = new GetDoctorsBySpesializationIdQuery(specializationId);
            return await SendRequestAsync(query);
        }

        [HttpGet("fullname")]
        [SwaggerOperation(Summary = "Find Doctor By FullName", OperationId = "FindDoctorByFullName")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DoctorAllDTO[]))]
        public async Task<ActionResult> FindDoctorByFullName([FromQuery] DoctorsFullNameDTO doctorsFullName)
        {
            var query = new GetDoctorByFullNameQuery(doctorsFullName);
            return await SendRequestAsync(query);
        }
    }
}
