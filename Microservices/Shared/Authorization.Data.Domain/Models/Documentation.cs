using Authorization.Data_Domain.Models.Abstraction;

namespace Authorization.Data_Domain.Models;

public class Documentation : KeyedEntityBase
{
    public string FileName { get; set; }
    public string Path { get; set; }
    public string ContainerName { get; set; }
    public virtual Office Office { get; set; }
    public virtual Account Account { get; set; }
}