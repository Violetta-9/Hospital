using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specialization.API.Application.Contracts.Incoming
{
    public class CreateSpecializationDTO
    {
        public string Title { get; set; }
        public virtual ICollection<long> ServicesId { get; set; }
    }
}
