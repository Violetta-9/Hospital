using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specialization.API.Application.Contracts.Outgoing
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public static Response Success => new() { IsSuccess = true };
        public static Response Error => new() { IsSuccess = false };
    }
}
