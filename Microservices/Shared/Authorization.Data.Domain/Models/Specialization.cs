using Authorization.Data_Domain.Models.Abstraction;

namespace Authorization.Data_Domain.Models;

public class Specialization : KeyedEntityBase
{
    public string Title { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<Doctor> Doctors { get; set; }
    public virtual ICollection<Service> Services { get; set; }
}