using Authorization.Data.Repository;
using MassTransit;
using MediatR;
using Services.API.Application.Command.UpdateServiceStatus;
using Services.API.Contracts.Incoming;
using SpecializationStatusChanged;

namespace Services.API.Consumer
{
    public class SpecializationStatusChangedConsumer: IConsumer<SpecializationStatusChanged.SpecializationStatusChanged>
    {
        private readonly IMediator _mediator;
        private readonly IServiceRepository _serviceRepository;
      


        public SpecializationStatusChangedConsumer(IMediator mediator,IServiceRepository serviceRepository)
        {
            _mediator = mediator;
            _serviceRepository = serviceRepository;
            
        }


        public async Task Consume(ConsumeContext<SpecializationStatusChanged.SpecializationStatusChanged> context)
        {

            var services = await _serviceRepository.GetServiceBySpecializationIdAsync(context.Message.SpecializationId);
            foreach (var service in services)
            {
                await _mediator.Send(
                    new UpdateServiceStatusCommand(new UpdateServiceStatusDTO()
                    {
                        Id= service.Id,
                        IsActive = context.Message.IsActive
                    }));
            }
        }
    }
}
