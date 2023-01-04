using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data_Domain.Models;

namespace Profile.Application.Contracts.Incoming
{
    public class DoctorDTO
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public long SpecializationId { get; set; }
        public long OfficeId { get; set; }
        public long StatusId { get; set; }
    }
}
