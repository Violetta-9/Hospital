using Microsoft.AspNetCore.Http;

namespace Office.Application.Contracts.Incoming;

public class CreateOfficeDTO
{
    public string Address { get; set; }
    public string RegistryPhoneNumber { get; set; }
    public IFormFile? File { get; set; }

}