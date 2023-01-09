namespace Profile.Application.Contracts.Incoming;

public class UpdateDoctorDTO
{
    public string AccountId { get; set; }
    public long SpecializationId { get; set; }
    public long OfficeId { get; set; }
}