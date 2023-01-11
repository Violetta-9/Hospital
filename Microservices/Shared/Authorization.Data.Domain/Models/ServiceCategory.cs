using Authorization.Data_Domain.Models.Abstraction;

namespace Authorization.Data_Domain.Models;

public class ServiceCategory : KeyedEntityBase
{
    public string Title { get; set; }
    public double TimeSlotSize { get; set; }
    public virtual ICollection<Service> Services { get; set; }
}