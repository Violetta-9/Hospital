using Authorization.Data_Domain.Models.Abstraction;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Data_Domain.Models
{
    public class Account:IdentityUser, IAudientable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birtday { get; set; }
        public DateTimeOffset RowCreatedTimestamp { get; set; }
        public DateTimeOffset LastRowModificationTimestamp { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Receptionist> Receptionists { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
