using Authorization.Data_Domain.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Data_Domain.Models
{
    public class Office: KeyedEntityBase
    {
        public string Address { get; set; }
        public string RegistryPhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Receptionist> Receptionists { get; set; }
    }
}
