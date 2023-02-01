using MediatR;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Photo.AddPhoto;

public class AddPhotoCommand : IRequest<Response>
{
    public AddPhotoDTO AddPhotoDTO { get; set; }


    public AddPhotoCommand(AddPhotoDTO addPhotoDTO)
    {
        AddPhotoDTO = addPhotoDTO;
    }
}