using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documents.API.Client.GeneratedClient;
using MediatR;
using Profile.Application.Contracts.Enum;
using Profile.Application.Contracts.Incoming;
using Response = Profile.Application.Contracts.Outgoing.Response;

namespace Profile.Application.Command.Photo.AddPhoto
{
    public class AddPhotoCommand:IRequest<Response>
    {
        public AddPhotoDTO AddPhotoDTO { get; set; }
       

        public AddPhotoCommand(AddPhotoDTO addPhotoDTO)
        {
            AddPhotoDTO = addPhotoDTO;
            
        }
    }
}
