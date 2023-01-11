using Authorization.Data.Repository;
using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Command.UpdateService;

internal class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, Response>
{
    private readonly IServiceRepository _serviceRepository;

    public UpdateServiceCommandHandler(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<Response> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetAsync(request.UpdateServiceDto.Id, cancellationToken);
        if (service == null) return Response.Error;
        service.Title = request.UpdateServiceDto.Title;
        service.Price = request.UpdateServiceDto.Price;
        service.IsActive = request.UpdateServiceDto.IsActive;
        service.ServiceCategoryId = request.UpdateServiceDto.ServiceCategoryId;
        await _serviceRepository.UpdateAsync(service, cancellationToken);
        return Response.Success;
    }
}