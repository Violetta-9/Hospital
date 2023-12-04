using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetServicesBySpecializationId
{
    public class GetServicesBySpecializationIdQuery : IRequest<OutServicesDto[]>
    {
        public long SpecializationId { get; set; }

        public GetServicesBySpecializationIdQuery(long specializationId)
        {
            SpecializationId = specializationId;
        }
    }
}
