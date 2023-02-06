using Authorization.Data_Domain.Models.Abstraction;

namespace Authorization.Data_Domain.Models;

public class Doctor : KeyedEntityBase
{
    public string AccountId { get; set; }
    public long SpecializationId { get; set; }
    public long OfficeId { get; set; }
    public DateTime CareerStartYear { get; set; }
    public long StatusId { get; set; }
    public virtual Status Status { get; set; }
    public virtual Account Account { get; set; }
    public virtual Office Office { get; set; }
    public virtual Specialization Specialization { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; }
}