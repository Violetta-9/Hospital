using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Profile.Application.Query.Patient.GetPatientIdByAccountId
{
    public class GetPatientIdByAccountIdQuery:IRequest<long>
    {
        public string AccountId { get; set; }

        public GetPatientIdByAccountIdQuery(string accountId)
        {
            AccountId = accountId;
        }
    }
}
