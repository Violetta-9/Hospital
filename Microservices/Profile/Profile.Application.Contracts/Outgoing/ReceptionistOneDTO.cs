namespace Profile.Application.Contracts.Outgoing;

public class ReceptionistOneDTO
{
    public string AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string OfficeAddress { get; set; }
    public long OfficeId { get; set; }
    public string DocumentAbsolutUrl { get; set; }
}