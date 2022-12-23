using Authorization.Data_Domain.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Data_Domain.Models
{
    public class Patient: KeyedEntityBase
    {
        public string AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
