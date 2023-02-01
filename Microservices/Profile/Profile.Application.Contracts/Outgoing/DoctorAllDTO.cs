namespace Profile.Application.Contracts.Outgoing;

public class DoctorAllDTO
{
    public long Id { get; set; }
    public string AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string SpecializationTitle { get; set; }
    public string OfficeAddress { get; set; }
    public string StatusTitle { get; set; }
    public DateTime BirthDay { get; set; }
    public string DocumentAbsolutUrl { get; set; }
}