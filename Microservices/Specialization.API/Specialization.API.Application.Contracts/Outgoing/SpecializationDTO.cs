namespace Specialization.API.Application.Contracts.Outgoing;

public class SpecializationDTO
{
    public long Id { get; set; }
    public string Title { get; set; }
    public bool IsActive { get; set; }
    public ICollection<OutServicesDto> Services { get; set; }
}