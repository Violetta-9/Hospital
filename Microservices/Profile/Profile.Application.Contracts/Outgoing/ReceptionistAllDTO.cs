namespace Profile.Application.Contracts.Outgoing;

public class ReceptionistAllDTO
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string OfficeAddress { get; set; }
    public string DocumentAbsolutUrl { get; set; }
}
