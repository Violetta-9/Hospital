using Authorization.Application.Contracts.Incoming.User;
using MediatR;

namespace Authorization.Application.Command.User.Registration;

public class RegistrationCommand : IRequest<string>
{
    public UserDTO User { get; set; }

    public RegistrationCommand(UserDTO user)
    {
        User = user;
    }
}