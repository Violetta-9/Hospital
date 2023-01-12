using Microsoft.Extensions.Options;
using Specialization.API.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specialization.API.Application.Services
{
    public interface IServicesService
    {

    }

    public class ServicesService : IServicesService
    {
        private readonly HttpClient _httpClient;
        private readonly UriSettings _uriSettings;
        public ServicesService(HttpClient httpClient, IOptions<UriSettings> uriSettings)
        {
            _httpClient = httpClient;
            _uriSettings = uriSettings.Value;
        }
    }
}
