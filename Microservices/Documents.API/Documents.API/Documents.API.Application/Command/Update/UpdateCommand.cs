using Documents.API.Application.Contracts.Incoming;
using MediatR;

namespace Documents.API.Application.Command.Update
{
    public class UpdateCommand:IRequest<long>
    {
        public UpdateFileDto FileDto { get; set; }

        public UpdateCommand(UpdateFileDto fileDto)
        {
            FileDto=fileDto;
        }
    }
}
