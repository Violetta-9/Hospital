using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.API.Contracts.Incoming
{
    public  class UpdateServiceStatusDTO
    {   
        public long Id { get; set; }
        public bool IsActive { get; set; }
       
    }
}
