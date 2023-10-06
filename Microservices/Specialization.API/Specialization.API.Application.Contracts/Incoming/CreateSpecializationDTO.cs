namespace Specialization.API.Application.Contracts.Incoming;

public class CreateSpecializationDTO
{
    public string Title { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<long> ServicesId { get; set; }
}