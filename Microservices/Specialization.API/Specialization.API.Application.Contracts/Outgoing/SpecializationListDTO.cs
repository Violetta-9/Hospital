namespace Specialization.API.Application.Contracts.Outgoing;

public class SpecializationListDTO
{
    public long Id { get; set; }
    public string Title { get; set; }
    public bool IsActive { get; set; }
    public OutServicesDto[] Services { get; set; }
}