using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.API.Contracts.Outgoing
{
    public class ServiceCategoriesDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public double TimeSlotSize { get; set; }

    }
}