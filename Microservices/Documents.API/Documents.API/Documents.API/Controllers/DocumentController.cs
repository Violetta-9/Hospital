using Documents.API.Application.Command.Delete;
using Documents.API.Application.Command.Update;
using Documents.API.Application.Command.Upload;
using Documents.API.Application.Command.UploadDocuments;
using Documents.API.Application.Contracts.Incoming;
using Documents.API.Application.Contracts.Outgoing;
using Documents.API.Application.Query.GetBlobDocuments;
using Documents.API.Application.Query.GetBlobPhoto;
using Documents.API.Controllers.Abstraction.Mediator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Response = Azure.Response;

namespace Documents.API.Controllers;

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
    public async Task<ActionResult> UploadBlob([FromForm] UploadFileDTO newFileDto)
    {
        IBaseRequest query;
        if (newFileDto.ResultId > 0)
        {
             query = new UploadDocumentsCommand(newFileDto);
        }
        else
        {
             query = new UploadCommand(newFileDto);
        }
       
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
    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation(Summary = "Get", OperationId = "GetBlob")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BlobDTO))]
    public async Task<ActionResult> GetBlob([FromQuery] DeleteOrGetFileDTO dto)
    {
        IBaseRequest query;
        if (dto.IsPhoto)
        {
            query=  new GetBlobPhotoQuery(dto);
            return await SendRequestAsync(query);
        }
        else
        {
            query = new GetBlobDocumentsQuery(dto);
        }
        return await SendRequestAsync(query);
    }
    [HttpPut]
    [SwaggerOperation(Summary = "Update", OperationId = "UpdateBlob")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(long))]
    public async Task<ActionResult> UpdateBlob([FromForm] UpdateFileDto newFileDto)
    {
        var query = new UpdateCommand(newFileDto);
        return await SendRequestAsync(query);
    }
}