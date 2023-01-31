using Documents.API.Application.Contracts.Enum;
using Microsoft.AspNetCore.Http;

namespace Documents.API.Application.Contracts.Incoming;

public class UploadFileDTO
{
    public IFormFile File { get; set; }
    public long EntityId { get; set; }
    public SubjectUpdate Subject { get; set; }
}