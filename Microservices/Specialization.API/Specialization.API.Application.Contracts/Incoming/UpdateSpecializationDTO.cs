using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specialization.API.Application.Contracts.Incoming
{
    public class UpdateSpecializationDTO
    {
        public long Id { get; set; }
        public ICollection<long> ServicesId { get; set; }
        public string Title { get; set; }
    }
}
