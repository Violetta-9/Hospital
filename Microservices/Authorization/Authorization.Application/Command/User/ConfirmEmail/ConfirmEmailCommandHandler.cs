using Authorization.Application.Contracts.Outgoing;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Application.Command.User.ConfirmEmail;

internal class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Response>
{
    private readonly UserManager<Account> _userManager;

    public ConfirmEmailCommandHandler(UserManager<Account> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Response> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        await _userManager.ConfirmEmailAsync(user, request.Token);
        return Response.Success;
    }
}