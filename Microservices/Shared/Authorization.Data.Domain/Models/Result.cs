using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data_Domain.Models.Abstraction;

namespace Authorization.Data_Domain.Models
{
    public  class Result:KeyedEntityBase
    {
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recomendations { get; set; }
        public long AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual Document Document { get; set; }
    }
}
