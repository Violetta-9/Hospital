using Microsoft.AspNetCore.Http;
using Documents.API.Application.Contracts.Enum;

namespace Documents.API.Application.Contracts.Incoming
{
    public class UploadFileDTO
    {
      
        public long EntityId { get; set; }
        public SubjectUpdate Subject { get; set; }
    }
}
