using MediatR;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Update.UpdatePassword;

public class UpdatePasswordCommand : IRequest<Response>
{
    public UpdatePasswordDTO NewPassword { get; set; }

    public UpdatePasswordCommand(UpdatePasswordDTO newPassword)
    {
        NewPassword = newPassword;
    }
}