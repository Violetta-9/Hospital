using MediatR;
using Office.Application.Contracts.Outgoing;

namespace Office.Application.Query.GetOfficeById
{
    public class GetOfficeByIdQuery:IRequest<OfficeDto>
    {
        public long Id { get; set; }

        public GetOfficeByIdQuery(long id)
        {
            Id = id;
        }
    }
}
