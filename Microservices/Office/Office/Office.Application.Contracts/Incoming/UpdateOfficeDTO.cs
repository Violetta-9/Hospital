using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office.Application.Contracts.Incoming
{
    public class UpdateOfficeDTO
    {
        public long OfficeId { get; set; }
        public string Address { get; set; }
        public string RegistryPhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
