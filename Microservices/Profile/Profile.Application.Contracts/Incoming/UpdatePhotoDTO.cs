using Microsoft.AspNetCore.Http;
using Profile.Application.Contracts.Enum;

namespace Profile.Application.Contracts.Incoming
{
    public class UpdatePhotoDTO
    {
        public string AccountId { get; set; }
        public SubjectUpdate SubjectUpdate { get; set; }
        public IFormFile File { get; set; }
    }
}
