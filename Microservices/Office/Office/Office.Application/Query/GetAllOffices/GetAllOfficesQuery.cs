using MediatR;
using Office.Application.Contracts.Outgoing;

namespace Office.Application.Query.GetAllOffices;

public class GetAllOfficesQuery : IRequest<AllOfficesDto[]>
{
}