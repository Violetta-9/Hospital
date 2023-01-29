using Documents.API.Application.Contracts.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.API.Application.Contracts.Incoming
{
    public class DeleteOrGetFileDTO
    {
        public long DocumentId { get; set; }
       
    }
}
