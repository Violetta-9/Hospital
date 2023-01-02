using Authorization.Data_Domain.Models.Abstraction;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Data_Domain.Models
{
    public class Account:IdentityUser, IAudientable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthday { get; set; }
        public DateTimeOffset RowCreatedTimestamp { get; set; }
        public DateTimeOffset LastRowModificationTimestamp { get; set; }
        public virtual Doctor Doctors { get; set; }
        public virtual Receptionist Receptionists { get; set; }
        public virtual Patient Patients { get; set; }
    }
}
