using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Specialization.API.Application.Contracts.Outgoing;
using Specialization.API.Application.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Specialization.API.Application.Services
{
    public interface IServicesService
    {
        public Task<Response> SetSpecializationIdForServicesAsync(long specializationId, ICollection<long> servicesId,
            CancellationToken cancellationToken);

        public Task<OutServicesDto[]> GetServicesBySpecializationId(long specializationId,
            CancellationToken cancellationToken);
    }

    public class ServicesService : IServicesService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UriSettings _uriSettings;
        private readonly IAccessTokenService _accessTokenService;
        public ServicesService(IHttpClientFactory httpClientFactory, IOptions<UriSettings> uriSettings, IAccessTokenService accessTokenService)
        {
            _httpClientFactory = httpClientFactory;
            _uriSettings = uriSettings.Value;
            _accessTokenService = accessTokenService;
        }

        public async Task<Response> SetSpecializationIdForServicesAsync(long specializationId,ICollection<long> servicesId,CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("ServicesClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessTokenService.GetAccessToken());

            var payload = new
            {
                SpecializationId = specializationId,
                ServicesId = servicesId
            };
            var strPayload = JsonConvert.SerializeObject(payload);
        
            HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync(_uriSettings.SetSpecializationIdPath,content,cancellationToken))
            {
               
                var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                   var error= JsonConvert.DeserializeObject<ResponseDetail>(responseString);
                   throw new Exception(error.Detail);

                }
                var responseObj = JsonConvert.DeserializeObject<Response>(responseString);
               return responseObj;
            }
           
        }
        public async Task<OutServicesDto[]> GetServicesBySpecializationId(long specializationId, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("ServicesClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessTokenService.GetAccessToken());

            var payload = new
            {
                SpecializationId = specializationId,
               
            };
            var strPayload = JsonConvert.SerializeObject(payload);

            HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");

            using (var response = await httpClient.GetAsync(string.Format(_uriSettings.GetServicesBySpecializationIdPath,specializationId), cancellationToken))
            {

                var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<ResponseDetail>(responseString);
                    throw new Exception(error.Detail);

                }
                var responseObj = JsonConvert.DeserializeObject<OutServicesDto[]>(responseString);
                return responseObj;
            }

        }
    }
}
