using Authorization.Data_Domain.Models.Abstraction;

namespace Authorization.Data_Domain.Models;

public class Patient : KeyedEntityBase
{
    public string AccountId { get; set; }
    public virtual Account Account { get; set; }
}