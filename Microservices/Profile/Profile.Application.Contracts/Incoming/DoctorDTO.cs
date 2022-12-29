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
        public string AccountId { get; set; }
        public long SpecializationId { get; set; }
        public long OfficeId { get; set; }
        public long StatusId { get; set; }
    }
}
