using Microsoft.AspNetCore.Http;

namespace Office.Application.Contracts.Incoming;

public class UpdateOfficeDTO
{
    public long OfficeId { get; set; }
    public string Address { get; set; }
    public string RegistryPhoneNumber { get; set; }
    public IFormFile File { get; set; }
   
}