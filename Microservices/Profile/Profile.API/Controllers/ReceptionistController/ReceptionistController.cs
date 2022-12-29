using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile.API.Controllers.Abstraction.Mediator;

using Profile.Application.Command.Receptionists.AddDoctorRole;
using Profile.Application.Command.Receptionists.AddPatientRole;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Helpers;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Command.Receptionists.DeleteDoctor;
using Profile.Application.Command.Receptionists.DeletePatient;
using Profile.Application.Command.Receptionists.UpdateOffice;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Command.Receptionists.DeleteReceptionist;

namespace Profile.API.Controllers.ReceptionistController
{
    [Route("api/[controller]")]
    [Authorize(Roles =$"{UserRoles.Admin},{UserRoles.Receptionist}" )]
    
    [ApiController]
    public class ReceptionistController : MediatingControllerBase
    {
        public ReceptionistController(IMediator mediator) : base(mediator)
        {

        }
      //todo: check
        [HttpPost("AddReceptionistRole")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerOperation(Summary = "Add receptionist role", OperationId = "AddReceptionistRole")]
        public async Task<ActionResult> AddReceptionistRole([FromBody] ReceptionistDTO receptionistDto)
        {
            var query = new AddReceptionistRoleCommand(receptionistDto);
            return await SendRequestAsync(query);
        }
        [HttpDelete("DeleteReceptionist")]
        [SwaggerOperation(Summary = "Delete Receptionist", OperationId = "DeleteReceptionist")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> DeleteReceptionist([FromForm] string accountId)
        {
            var query = new DeleteReceptionistCommand(accountId);
            return await SendRequestAsync(query);
        }

        [HttpPost("AddDoctorRole")]
        [SwaggerOperation(Summary = "Add patient role", OperationId = "AddDoctorRole")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<ActionResult> AddDoctorRole([FromBody] DoctorDTO doctor)
        {
            var query = new AddDoctorRoleCommand(doctor);
            return await SendRequestAsync(query);
        }
        [HttpDelete("DeleteDoctor")]
        [SwaggerOperation(Summary = "Delete Doctor", OperationId = "DeleteDoctor")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<ActionResult> DeleteDoctor([FromBody] string accountId)
        {
            var query = new DeleteDoctorCommand(accountId);
            return await SendRequestAsync(query);
        }
        [HttpDelete("DeletePatient")]
        [SwaggerOperation(Summary = "Delete Patient", OperationId = "DeletePatient")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<ActionResult> DeletePatient([FromForm] string accountId)
        {
            var query = new DeletePatientCommand(accountId);
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
        [HttpPatch("UpdateOffice")]
        [SwaggerOperation(Summary = "Update Office", OperationId = "UpdateOffice")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<ActionResult> UpdateOffice([FromBody] string userId,long officeId)
        {
            var query = new UpdateOfficeCommand(userId, officeId);
            return await SendRequestAsync(query);
        }


    }
}
