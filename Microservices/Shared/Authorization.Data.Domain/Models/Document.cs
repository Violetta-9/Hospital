using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data_Domain.Models.Abstraction;

namespace Authorization.Data_Domain.Models
{
    public class Document:KeyedEntityBase
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public string ContainerName { get; set; }
        public long ResultId { get; set; }
        public virtual Result Result { get; set; }
    }
}
