

using System.Globalization;
using Newtonsoft.Json;
using Profile.Application.Contracts.Incoming;
using System.Text;
using Microsoft.Extensions.Options;
using Profile.Application.Helpers;


namespace Profile.Application.Services
{
    public interface IAuthorizationService
    {
        public Task<string> SendDoctorInfoForRegistrationAsync(DoctorDTO request, string password,
            CancellationToken cancellationToken);

        public Task<string> SendReceptionistInfoForRegistrationAsync(ReceptionistDTO request, string password,
            CancellationToken cancellationToken);

    }

    public class AuthorizationService : IAuthorizationService
    {
        private readonly HttpClient _httpClient;
        private readonly UriSettings _uriSettings;

        public AuthorizationService(HttpClient httpClient,IOptions<UriSettings> uriSettings)
        {
            _httpClient = httpClient;
            _uriSettings = uriSettings.Value;
        }
        public async Task<string> SendDoctorInfoForRegistrationAsync(DoctorDTO request, string password, CancellationToken cancellationToken)
        {

            var payload = new Dictionary<string, string>
            {
                {"FirstName", request.FirstName},
                {"LastName", request.LastName},
                {"MiddleName", request.MiddleName},
                {"Email",request.Email},
                {"Password", password},
                {"PhoneNumber",request.PhoneNumber},
                {"BirthDate", request.BirthDate.ToString("s")}

            };
            string strPayload = JsonConvert.SerializeObject(payload);

            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_uriSettings.RegistrationPath, c, cancellationToken);
            var responseStream = await response.Content.ReadAsStringAsync(cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseStream);
            }

            return responseStream;
        }
        public async Task<string> SendReceptionistInfoForRegistrationAsync(ReceptionistDTO request, string password, CancellationToken cancellationToken)
        {

            
            var payload = new Dictionary<string, string>
            {
                {"FirstName", request.FirstName},
                {"LastName", request.LastName},
                {"MiddleName", request.MiddleName},
                {"Email",request.Email},
                {"Password", password},
                {"PhoneNumber",request.PhoneNumber},
                {"BirthDate", request.BirthDate.ToString("s")}
            };
            string strPayload = JsonConvert.SerializeObject(payload);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            var a = _uriSettings.RegistrationPath;
            var response = await _httpClient.PostAsync(_uriSettings.RegistrationPath, c, cancellationToken);
            var responseStream = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseStream);
            }

            return responseStream;
        }
    }
}
