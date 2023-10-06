using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Profile.Application.Query.Receptionist.GetReceptionistIdByAccountId
{
    public class GetReceptionistIdByAccountIdQuery:IRequest<long>
    {
        public string AccountId { get; set; }

        public GetReceptionistIdByAccountIdQuery(string accountId)
        {
            AccountId=accountId;
        }
    }
}
