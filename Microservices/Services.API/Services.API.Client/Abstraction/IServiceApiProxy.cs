using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.API.Client.GeneratedClient;

namespace Services.API.Client.Abstraction
{
    public interface IServiceApiProxy
    {
        public Task<Response> SetSpecializationIdForServicesAsync(long specializationId, ICollection<long> servicesId,
            CancellationToken cancellationToken);
    }
}
