using Authorization.Data_Domain.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Data_Domain.Models
{
    public class Status: KeyedEntityBase
    {
        public string Title { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
