using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specialization.API.Application.Contracts.Incoming
{
    public class UpdateSpecializationStatusDTO
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
    }
}
