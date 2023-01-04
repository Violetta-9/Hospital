

namespace Profile.Application.Contracts.Incoming
{
    public class ReceptionistDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public long OfficeId { get; set; }
    }
}
