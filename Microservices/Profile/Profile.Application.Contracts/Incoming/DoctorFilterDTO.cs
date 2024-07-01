

namespace Profile.Application.Contracts.Incoming
{
    public class DoctorFilterDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public long? SpecializationId { get; set; }
        public long? OfficeId { get; set; }
    }
}
