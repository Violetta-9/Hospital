using MediatR;
using Services.API.Contracts.Incoming;

namespace Services.API.Application.Command.CreateService;

public class CreateServiceCommand : IRequest<long>
{
    public CreateServiceDTO CreateServiceDto { get; set; }

    public CreateServiceCommand(CreateServiceDTO createServiceDto)
    {
        CreateServiceDto = createServiceDto;
    }
}