using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specialization.API.Application.Helpers
{
    public class UriSettings
    {
        public string BasedAddressForService { get; set; }
        public string SetSpecializationIdPath { get; set; }
        public string GetServicesBySpecializationIdPath { get; set; }
        public string BasedAddressForDoctors { get; set; }
        public string ChangeDoctorStatus { get; set; }
        public string ChangeServiceStatus { get; set; }
    }
}
