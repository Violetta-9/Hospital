using MediatR;
using Specialization.API.Application.Contracts.Outgoing;

namespace Specialization.API.Application.Query.GetAllSpecialization;

public class GetAllSpecializationQuery : IRequest<SpecializationListDTO[]>
{
}