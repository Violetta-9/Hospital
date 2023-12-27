using Appointment.API.Application.Contracts.Incoming;
using Appointment.API.Application.Contracts.Outgoing;
using Appointment.API.Application.Service;
using Authorization.Data_Domain.Models.Entity;
using AutoMapper;
using MediatR;

namespace Appointment.API.Application.Command.Result.CreateAppointment
{
    public class CreateAppointmentResultCommandHandler : IRequestHandler<CreateAppointmentResultCommand,Response>
    {
        private readonly IPdfService _pdfService;
        private readonly IMapper _mapper;

        public CreateAppointmentResultCommandHandler(IPdfService service){
            _pdfService =service;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateAppointmentResultDto, CreateAppointmentResult>();
            });

            _mapper = config.CreateMapper();
        }
        public async Task<Response> Handle(CreateAppointmentResultCommand request, CancellationToken cancellationToken)
        {
           return await _pdfService.GeneratePDF(_mapper.Map<CreateAppointmentResult>(request.CreateAppointmentResultDto));
        }
    }
}
