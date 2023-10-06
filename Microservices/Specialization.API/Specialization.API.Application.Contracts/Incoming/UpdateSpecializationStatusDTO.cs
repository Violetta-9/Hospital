namespace Specialization.API.Application.Contracts.Incoming;

public class UpdateSpecializationStatusDTO
{
    public long Id { get; set; }
    public bool IsActive { get; set; }
}