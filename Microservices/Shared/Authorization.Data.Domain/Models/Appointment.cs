using Authorization.Data_Domain.Models.Abstraction;

namespace Authorization.Data_Domain.Models;

public class Appointment : KeyedEntityBase
{
    public long PatientId { get; set; }
    public long DoctorId { get; set; }
    public long ServiceId { get; set; }
    public long SpecializationId { get; set; }
    public long OfficeId { get; set; }
    public bool IsApproved { get; set; }
    public DateTime DateTime { get; set; }
    public virtual Patient Patient { get; set; }
    public virtual Office Office { get; set; }
    public virtual Specialization Specialization { get; set; }
    public virtual Doctor Doctor { get; set; }
    public virtual Service Service { get; set; }
    public virtual Result Result { get; set; }
}