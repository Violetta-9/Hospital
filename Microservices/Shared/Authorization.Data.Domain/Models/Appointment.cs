using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data_Domain.Models.Abstraction;
using Microsoft.VisualBasic;

namespace Authorization.Data_Domain.Models
{
    public class Appointment:KeyedEntityBase
    {
        public long PatientId { get; set; }
        public long DoctorId { get; set; }
        public long ServiceId { get; set; }
        public bool IsApproved { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Service Service { get; set; }
        public virtual Result Result { get; set; }
    }
}
