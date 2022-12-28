﻿using Authorization.Data_Domain.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Data_Domain.Models
{
    public class Doctor: KeyedEntityBase
    {
        public string AccountId { get; set; }
        public long SpecializationId { get; set; }
        public long OfficeId { get; set; }
        public DateTime CareerStartYear { get; set; }
        public bool Status { get; set; }
        public virtual Account Account { get; set; }
        public virtual Office Office { get; set; }
        public virtual Specialization Specialization { get; set; }
    }
}