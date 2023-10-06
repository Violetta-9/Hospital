using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Profile.Application.Query.Doctor.GetDoctorIdByAccountId
{
    public class GetDoctorIdByAccountIdQuery:IRequest<long>
    {
        public string AccountId { get; set; }

        public GetDoctorIdByAccountIdQuery(string accountId)
        {
            AccountId = accountId;
        }
    }

}
