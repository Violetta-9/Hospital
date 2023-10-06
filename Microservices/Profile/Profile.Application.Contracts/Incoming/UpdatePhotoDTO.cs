using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Application.Contracts.Incoming
{
    public class UpdatePhotoDTO
    {
        public string AccountId { get; set; }
        public IFormFile File { get; set; }
    }
}
