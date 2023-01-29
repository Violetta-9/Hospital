using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using Documents.API.Controllers.Abstraction.Mediator;
using Profile.Application.Helpers;
using MediatR;
using Documents.API.Application.Command.Delete;
using Documents.API.Application.Command.Upload;
using Documents.API.Application.Contracts.Incoming;
using Documents.API.Application.Contracts.Outgoing;
using Documents.API.Application.Query.GetBlob;
using Office.Application.Contracts.Incoming;
using Swashbuckle.AspNetCore.Annotations;
using Response = Azure.Response;
using Microsoft.AspNetCore.Http;

namespace Documents.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : MediatingControllerBase
    {
        public DocumentController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        [SwaggerOperation(Summary = "Upload", OperationId = "UploadBlob")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
        public async Task<ActionResult> UploadBlob([FromForm] UploadFileDTO newFileDto, [FromForm] IFormFile file)
        {
            var query = new UploadCommand(newFileDto,file);
            return await SendRequestAsync(query);
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete", OperationId = "DeleteBlob")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response))]
        public async Task<ActionResult> DeleteBlob([FromBody] DeleteOrGetFileDTO dto)
        {
            var query = new DeleteCommand(dto);
            return await SendRequestAsync(query);
        }
        [HttpGet]
        [SwaggerOperation(Summary = "Get", OperationId = "GetBlob")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BlobDTO))]
        public async Task<ActionResult> GetBlob([FromQuery] DeleteOrGetFileDTO dto)
        {
            var query = new GetBlobQuery(dto);
            return await SendRequestAsync(query);
        }
    }
}
