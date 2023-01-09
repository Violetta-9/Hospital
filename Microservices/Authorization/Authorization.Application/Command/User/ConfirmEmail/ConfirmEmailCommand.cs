using Authorization.Application.Contracts.Outgoing;
using MediatR;

namespace Authorization.Application.Command.User.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<Response>
{
    public string UserId { get; set; }
    public string Token { get; set; }

    public ConfirmEmailCommand(string userId, string token)
    {
        UserId = userId;
        Token = token;
    }
}