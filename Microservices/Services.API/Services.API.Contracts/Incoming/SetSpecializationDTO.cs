namespace Services.API.Contracts.Incoming;

public class SetSpecializationDTO
{
    public long SpecializationId { get; set; }
    public  ICollection<long> ServicesId { get; set; }
}