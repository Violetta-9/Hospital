using MediatR;
using Specialization.API.Application.Contracts.Incoming;

namespace Specialization.API.Application.Command.CreateSpecialization;

public class CreateSpecializationCommand : IRequest<long>
{
    public CreateSpecializationDTO CreateSpecializationDto { get; set; }

    public CreateSpecializationCommand(CreateSpecializationDTO createSpecialization)
    {
        CreateSpecializationDto = createSpecialization;
    }
}