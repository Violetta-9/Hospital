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
using Profile.Application.Query.Doctor.GetAllDoctors;
using Profile.Application.Command.Receptionists.AddDoctorRole;
using Profile.Application.Command.Receptionists.DeleteDoctor;
using Profile.Application.Query.Doctor.GetDoctorById;

namespace Profile.API.Controllers.ReceptionistController
{
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoles.Receptionist)]
    [ApiController]
    public class DoctorController : MediatingControllerBase
    {
        public DoctorController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPatch("UpdateStatus")]
        [SwaggerOperation(Summary = "Update Status", OperationId = "UpdateStatus")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> UpdateStatus([FromForm] long status, [FromForm] string accountId)
        {
            var query = new UpdateDoctorStatusCommand(status, accountId);
            return await SendRequestAsync(query);
        }
        [HttpPatch("UpdateDoctorProfile")]
        [SwaggerOperation(Summary = "Update Doctor Profile", OperationId = "UpdateDoctorProfile")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> UpdateDoctorProfile([FromBody] UpdateDoctorDTO updateDoctorDTO)
        {
            var query = new UpdateDoctorProfileCommand(updateDoctorDTO);
            return await SendRequestAsync(query);
        }
        [HttpGet("GetAllDoctors")]
        [SwaggerOperation(Summary = "Get All Doctors", OperationId = "GetAllDoctors")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DoctorAllDTO[]))]
        public async Task<ActionResult> GetAllDoctors()
        {
            var query =new GetAllDoctorsQuery();
            return await SendRequestAsync(query);
        }
        [HttpGet("GetDoctorById")]
        [SwaggerOperation(Summary = "Get Doctor By Id", OperationId = "GetDoctorById")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(DoctorOneDTO))]
        public async Task<ActionResult> GetDoctorById([FromQuery] long doctorId)
        {
            var query = new GetDoctorByIdQuery(doctorId);
            return await SendRequestAsync(query);
        }
        [HttpPost("AddDoctorRole")]
        [SwaggerOperation(Summary = "Add patient role", OperationId = "AddDoctorRole")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> AddDoctorRole([FromBody] DoctorDTO doctor)
        {
            var query = new AddDoctorRoleCommand(doctor);
            return await SendRequestAsync(query);
        }
        [HttpDelete("DeleteDoctor")]
        [SwaggerOperation(Summary = "Delete Doctor", OperationId = "DeleteDoctor")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> DeleteDoctor([FromBody] string accountId)
        {
            var query = new DeleteDoctorCommand(accountId);
            return await SendRequestAsync(query);
        }
    }
}
