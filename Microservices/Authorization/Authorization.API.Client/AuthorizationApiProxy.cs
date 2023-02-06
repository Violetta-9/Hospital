using System.ComponentModel.DataAnnotations;
using Authorization.API.Client.Abstraction;
using Authorization.API.Client.Configuration;
using Authorization.API.Client.GeneratedClient;
using Authorization.API.Client.HttpClientProvider.Abstraction;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ValidationException = FluentValidation.ValidationException;

namespace Authorization.API.Client
{
    public class AuthorizationApiProxy:IAuthorizationApiProxy
    {
       
        private string BaseUrl { get; }
        private readonly IAuthorizationApiHttpClientProvider _httpClientProvider;

        public AuthorizationApiProxy( IOptions<AuthorizationApiOptions> serviceOptions,IAuthorizationApiHttpClientProvider httpClientProvider)
        {
            _httpClientProvider= httpClientProvider;
            BaseUrl = serviceOptions.Value.AuthorizationUrl;
           
        }

        public async Task<string> RegistrationAsync(UserDTO newUser,CancellationToken cancellationToken=default)
        {
            var api = await GetApiClientAsync(cancellationToken);
            try
            {
                var response = await api.RegisterUserAsync(newUser,cancellationToken);
                return response.AccountId;
            }
            catch (ApiException e)
            {
                var error = JsonConvert.DeserializeObject<ResponseDetail.ResponseDetail>(e.Response);
                throw new ValidationException(new List<ValidationFailure>
                {
                    new(string.Empty, error?.Detail)
                });
            }
        }

        public async Task<AuthorizationApi> GetApiClientAsync(CancellationToken cancellationToken = default)
        {
            return new AuthorizationApi(BaseUrl, await _httpClientProvider.GetHttpClientAsync(cancellationToken));
        }
    }
}
