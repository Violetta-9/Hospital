

using System.Globalization;
using Newtonsoft.Json;
using Profile.Application.Contracts.Incoming;
using System.Text;


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

        public AuthorizationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
            var response = await _httpClient.PostAsync("api/User/registration", c, cancellationToken);
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
            var response = await _httpClient.PostAsync("api/User/registration", c, cancellationToken);
            var responseStream = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseStream);
            }

            return responseStream;
        }
    }
}
