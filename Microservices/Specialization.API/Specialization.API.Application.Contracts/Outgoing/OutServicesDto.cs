using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specialization.API.Application.Contracts.Outgoing
{
    public class OutServicesDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
        public string ServiceCategoryName { get; set; }
    }
}
