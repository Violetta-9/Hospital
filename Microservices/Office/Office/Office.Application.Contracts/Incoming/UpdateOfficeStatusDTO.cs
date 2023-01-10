using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office.Application.Contracts.Incoming
{
    public class UpdateOfficeStatusDTO
    {
        public long OfficeId { get; set; }
        public bool IsActive { get; set; }
    }
}
