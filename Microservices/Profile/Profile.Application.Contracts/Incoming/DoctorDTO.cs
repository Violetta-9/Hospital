namespace Profile.Application.Contracts.Incoming
{
    public class DoctorDTO
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public long SpecializationId { get; set; }
        public long OfficeId { get; set; }
        public long StatusId { get; set; }
    }
}
