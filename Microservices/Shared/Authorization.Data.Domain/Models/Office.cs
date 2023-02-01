using Authorization.Data_Domain.Models.Abstraction;

namespace Authorization.Data_Domain.Models;

public class Office : KeyedEntityBase
{
    public string Address { get; set; }
    public string RegistryPhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public long? DocumentationId { get; set; }
    public virtual ICollection<Doctor> Doctors { get; set; }
    public virtual ICollection<Receptionist> Receptionists { get; set; }
    public virtual Documentation Documentation { get; set; }
}