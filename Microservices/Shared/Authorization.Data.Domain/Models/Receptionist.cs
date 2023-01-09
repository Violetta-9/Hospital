using Authorization.Data_Domain.Models.Abstraction;

namespace Authorization.Data_Domain.Models;

public class Receptionist : KeyedEntityBase
{
    public string AccountId { get; set; }
    public long OfficeId { get; set; }
    public virtual Account Account { get; set; }
    public virtual Office Office { get; set; }
}