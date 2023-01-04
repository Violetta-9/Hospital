

using Newtonsoft.Json;
using Profile.Application.Contracts.Incoming;
using System.Text;


namespace Profile.Application.Services
{
    public interface IAuthorizationService
    {
        public Task<string> SendDoctorInfoForRegistrationAsync(DoctorDTO request, string password,
            CancellationToken cancellationToken);

        public Task<string> SendReceptionistInfoForRegistrationAsync(ReceptionistDTO request,string password,
            CancellationToken cancellationToken);

    }

    public  class AuthorizationService:IAuthorizationService
    {
        public async Task<string> SendDoctorInfoForRegistrationAsync(DoctorDTO request, string password, CancellationToken cancellationToken)
        {
         
            var response = string.Empty;
            Uri u = new Uri("https://localhost:44336/api/User/registration");
            var payload = new Dictionary<string, string>
            {
                {"FirstName", $"{request.FirstName}"},
                {"LastName", $"{request.LastName}"},
                {"MiddleName", $"{request.MiddleName}"},
                {"Email",$"{request.Email}"},
                {"Password", $"{password}"},
                {"PhoneNumber",$"{request.PhoneNumber}"},
                {"Day",$"{request.Day}"},
                {"Month",$"{request.Month}"},
                {"Year",$"{request.Year}"}
            };
            string strPayload = JsonConvert.SerializeObject(payload);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                HttpRequestMessage AuthorRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = u,
                    Content = c
                };
                HttpResponseMessage result = await client.SendAsync(AuthorRequest, cancellationToken);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync(cancellationToken);

                }
            }

            return response;
        }
        public async Task<string> SendReceptionistInfoForRegistrationAsync(ReceptionistDTO request,string password, CancellationToken cancellationToken)
        {
          
            var response = string.Empty;
            Uri u = new Uri("https://localhost:44336/api/User/registration");
            var payload = new Dictionary<string, string>
            {
                {"FirstName", $"{request.FirstName}"},
                {"LastName", $"{request.LastName}"},
                {"MiddleName", $"{request.MiddleName}"},
                {"Email",$"{request.Email}"},
                {"Password", $"{password}"},
                {"PhoneNumber",$"{request.PhoneNumber}"},
                {"Day",$"{request.Day}"},
                {"Month",$"{request.Month}"},
                {"Year",$"{request.Year}"}
            };
            string strPayload = JsonConvert.SerializeObject(payload);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                HttpRequestMessage AuthorRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = u,
                    Content = c
                };
                HttpResponseMessage result = await client.SendAsync(AuthorRequest, cancellationToken);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync(cancellationToken);

                }
            }

            return response;
        }
    }
}
