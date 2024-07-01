namespace Profile.Application.Contracts.Outgoing;

public class DoctorOneDTO
{
    public string AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string SpecializationTitle { get; set; }
    public string OfficeAddress { get; set; }
    public string StatusTitle { get; set; }
    public string DocumentAbsolutUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public string Blob { get; set; }
    public DateTime CareerStartYear { get; set; }
    public DateTime BirthDay { get; set; }
}