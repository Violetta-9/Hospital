

using Authorization.Data_Domain.Models;

namespace Profile.Application.Contracts.Outgoing
{
    public class ResultForPatient
    {
        public string Title { get; set; }
        public List<ServiceForPatientResultDto> Children { get; set; }
    }
}
