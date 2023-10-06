using Services.API.Client.GeneratedClient;

namespace Services.API.Client.Abstraction;

public interface IServiceApiProxy
{
    public Task<Response> SetSpecializationIdForServicesAsync(long specializationId, ICollection<long> servicesId,
        CancellationToken cancellationToken);
    public Task<Response> UpdateSpecializationIdForServicesAsync(long specializationId, ICollection<long> servicesId,
        CancellationToken cancellationToken);
}