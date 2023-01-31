using Profile.Application.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Application.Contracts.Incoming
{
    public class DeletePhotoDTO
    {
        public string AccountId { get; set; }
        public SubjectUpdate SubjectUpdate { get; set; }
    }
}
