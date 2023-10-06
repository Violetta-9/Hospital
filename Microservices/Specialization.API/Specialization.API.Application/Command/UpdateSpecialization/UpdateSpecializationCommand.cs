using MediatR;
using Specialization.API.Application.Contracts.Incoming;
using Specialization.API.Application.Contracts.Outgoing;

namespace Specialization.API.Application.Command.UpdateSpecialization;

public class UpdateSpecializationCommand : IRequest<Response>
{
    public long Id { get; set; }
    public ICollection<long> ServicesId { get; set; }
    public string Title { get; set; }

    public UpdateSpecializationCommand(UpdateSpecializationDTO updateSpecializationDto)
    {
        Id = updateSpecializationDto.Id;
        Title = updateSpecializationDto.Title;
        ServicesId = updateSpecializationDto.ServicesId;
    }
}