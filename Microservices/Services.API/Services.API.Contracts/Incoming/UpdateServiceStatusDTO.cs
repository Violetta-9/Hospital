namespace Services.API.Contracts.Incoming;

public class UpdateServiceStatusDTO
{
    public long Id { get; set; }
    public bool IsActive { get; set; }
}