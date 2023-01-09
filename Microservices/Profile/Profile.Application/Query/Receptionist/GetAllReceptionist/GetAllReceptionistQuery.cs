using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Receptionist.GetAllReceptionist;

public class GetAllReceptionistQuery : IRequest<ReceptionistAllDTO[]>
{
}