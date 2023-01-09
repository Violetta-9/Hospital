using Authorization.Data_Domain.Models.Abstraction;

namespace Authorization.Data_Domain.Models;

public class Status : KeyedEntityBase
{
    public string Title { get; set; }
    public virtual ICollection<Doctor> Doctors { get; set; }
}