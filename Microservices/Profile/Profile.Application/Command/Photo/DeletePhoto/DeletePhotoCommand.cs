using MediatR;
using Profile.Application.Contracts.Enum;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Photo.DeletePhoto;

public class DeletePhotoCommand : IRequest<Response>
{
    public string AccountId { get; set; }
    public SubjectUpdate SubjectUpdate { get; set; }

    public DeletePhotoCommand(DeletePhotoDTO delPhoto)
    {
        AccountId = delPhoto.AccountId;
        SubjectUpdate = delPhoto.SubjectUpdate;
    }
}