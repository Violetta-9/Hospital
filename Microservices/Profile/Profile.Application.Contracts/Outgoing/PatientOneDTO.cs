using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Application.Contracts.Outgoing
{
    public class PatientOneDTO
    {
        public string AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public string PhotoUrl { get; set; }
    }
}
