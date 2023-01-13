using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using MediatR;

namespace Services.API.Application.Command.CreateService;

internal class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, long>
{
    private readonly IServiceRepository _serviceRepository;

    public CreateServiceCommandHandler(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<long> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = new Service
        {
            Title = request.CreateServiceDto.Title,
            Price = request.CreateServiceDto.Price,
            IsActive = request.CreateServiceDto.IsActive,
            ServiceCategoryId = request.CreateServiceDto.ServiceCategoryId
        };
        await _serviceRepository.InsertAsync(service, cancellationToken);
        return service.Id;
    }
}