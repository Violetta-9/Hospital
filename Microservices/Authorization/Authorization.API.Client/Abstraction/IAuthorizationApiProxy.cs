
using Authorization.API.Client.GeneratedClient;

namespace Authorization.API.Client.Abstraction;

public interface IAuthorizationApiProxy
{
    public  Task<string> RegistrationAsync(UserDTO newUser, CancellationToken cancellationToken = default);


}