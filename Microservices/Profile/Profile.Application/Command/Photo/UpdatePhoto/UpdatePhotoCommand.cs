
using MediatR;
using Microsoft.AspNetCore.Http;
using Profile.Application.Contracts.Enum;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Photo.UpdatePhoto
{
    public class UpdatePhotoCommand:IRequest<Response>
    {
        public string AccountId { get; set; }
        public IFormFile File { get; set; }
        public SubjectUpdate SubjectUpdate { get; set; }

        public UpdatePhotoCommand(UpdatePhotoDTO photo)
        {
            AccountId=photo.AccountId;
            File = photo.File;
            SubjectUpdate = photo.SubjectUpdate;
        }
    }
}
