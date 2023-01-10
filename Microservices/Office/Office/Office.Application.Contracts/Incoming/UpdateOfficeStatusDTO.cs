namespace Office.Application.Contracts.Incoming;

public class UpdateOfficeStatusDTO
{
    public long OfficeId { get; set; }
    public bool IsActive { get; set; }
}