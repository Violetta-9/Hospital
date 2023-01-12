using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.API.Contracts.Incoming
{
    public class SetSpecializationDTO
    {
        public long SpecializationId { get; set; }
        public virtual ICollection<long> ServicesId { get; set; }
    }
}
