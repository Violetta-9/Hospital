using Authorization.Application.Contracts.Incoming.User;
using Authorization.Application.Contracts.Outgoing;
using MediatR;

namespace Authorization.Application.Command.User.Registration;

public class RegistrationCommand : IRequest<AccessToken>
{
    public UserDTO User { get; set; }

    public RegistrationCommand(UserDTO user)
    {
        User = user;
    }
}