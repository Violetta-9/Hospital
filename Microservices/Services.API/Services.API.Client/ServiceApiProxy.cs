
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Services.API.Client.Abstraction;
using Services.API.Client.Configuration;
using Services.API.Client.GeneratedClient;
using Services.API.Client.HttpClientProvider.Abstraction;

namespace Services.API.Client
{
    public class ServiceApiProxy : IServiceApiProxy
    {
        private string BaseUrl { get; set; }
        private readonly IServiceApiHttpClientProvider _httpClientProvider;
        public ServiceApiProxy(IServiceApiHttpClientProvider httpClientProvider, IOptions<ServiceApiOptions> serviceOptions)
        {
            BaseUrl = serviceOptions.Value.ServiceUrl;
            _httpClientProvider = httpClientProvider;
        }

        public async Task<Response> SetSpecializationIdForServicesAsync(long specializationId,
            ICollection<long> servicesId, CancellationToken cancellationToken)
        {
            var api = await GetApiClientAsync(cancellationToken);
            try
            {
                var response = await api.SetSpecializationForServiceAsync(new SetSpecializationDTO()
                {
                    ServicesId = servicesId,
                    SpecializationId = specializationId
                }, cancellationToken);
                return response;
            }
            catch (ApiException e)
            {
                var error = JsonConvert.DeserializeObject<ResponseDetail.ResponseDetail>(e.Response);
                    throw new ValidationException(new List<ValidationFailure>()
                    {
                        new(string.Empty, error?.Detail)
                    });
                
               
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ServiceApi> GetApiClientAsync(CancellationToken cancellationToken = default)
        {
            return new ServiceApi(BaseUrl, await _httpClientProvider.GetHttpClientAsync(cancellationToken));
        }
    }
}
