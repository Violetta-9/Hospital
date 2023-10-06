using MediatR;
using Specialization.API.Application.Contracts.Outgoing;

namespace Specialization.API.Application.Query.GetSpecializationById;

public class GetSpecializationByIdQuery : IRequest<SpecializationDTO>
{
    public long SpecializationId { get; set; }

    public GetSpecializationByIdQuery(long specializationId)
    {
        SpecializationId = specializationId;
    }
}