using Authorization.Application.Contracts.Outgoing;
using Authorization.Application.Helpers;
using Authorization.Application.Resources;
using Authorization.Application.Services;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Application.Command.User.Registration;

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, AuthorizationId>
{
    private readonly IEmailServices _emailServices;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<Account> _userManager;

    public RegistrationCommandHandler(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager,
        IEmailServices emailServices)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _emailServices = emailServices;
    }

    public async Task<AuthorizationId> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        var role = UserRoles.User;

        var appUser = new Account
        {
            FirstName = request.User.FirstName,
            LastName = request.User.LastName,
            MiddleName = request.User.MiddleName,
            UserName = request.User.Email,
            Email = request.User.Email,
            PhoneNumber = request.User.PhoneNumber,
            Birthday = request.User.BirthDate
        };

        if (!await _roleManager.RoleExistsAsync(role))
        {
            var newRole = new IdentityRole(role);
            await _roleManager.CreateAsync(newRole);
        }

        var result = await _userManager.CreateAsync(appUser, request.User.Password);
        await _userManager.AddToRoleAsync(appUser, role);

        if (result.Succeeded)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);

            await _emailServices.SendConfirmEmailAsync(appUser, Messages.EmailSubject,
                code,
                cancellationToken);
            return new AuthorizationId() { AccountId = appUser.Id };
        }

        throw new Exception(string.Join("/n", result.Errors.Select(x => x.Description)));
    }
}