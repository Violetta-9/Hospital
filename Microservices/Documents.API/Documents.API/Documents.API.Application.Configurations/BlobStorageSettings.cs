using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.API.Application.Configurations
{
    public class BlobStorageSettings
    {
        public string DoctorPathTemplate { get; set; }
        public string ReceptionistPathTemplate { get; set; }
        public string PatientPathTemplate { get; set; }
        public string OfficePathTemplate { get; set; }
        public string ImagesContainer { get; set; }
    }
}
