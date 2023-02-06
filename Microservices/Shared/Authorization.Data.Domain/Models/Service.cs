using Authorization.Data_Domain.Models.Abstraction;

namespace Authorization.Data_Domain.Models;

public class Service : KeyedEntityBase
{
    public string Title { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }
    public long? SpecializationId { get; set; }
    public long ServiceCategoryId { get; set; }
    public virtual Specialization Specialization { get; set; }
    public virtual ServiceCategory ServiceCategory { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; }
}