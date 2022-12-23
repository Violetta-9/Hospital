using Authorization.Data_Domain.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Data_Domain.Models
{
    public  class Receptionist: KeyedEntityBase
    {
        public string AccountId { get; set; }
        public long OfficeId { get; set; }
        public virtual Account Account { get; set; }
        public virtual Office Office { get; set; }
       
    }
}
