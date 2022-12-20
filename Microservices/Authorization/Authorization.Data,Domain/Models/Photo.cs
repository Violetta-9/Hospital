
using Authorization.Data_Domain.Models.Abstraction;

namespace Authorization.Data_Domain.Models
{
    public class Photo: KeyedEntityBase
    {
        public string Url { get; set; }
        public virtual Account Account { get; set; }
    }
}
