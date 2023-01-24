using Authorization.API.Client.Abstraction;
using Authorization.API.Client.GeneratedClient;
using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;
using Profile.Application.Services;

namespace Profile.Application.Command.Receptionists.AddReceptionistRole;

public class AddReceptionistRoleCommandHandler : IRequestHandler<AddReceptionistRoleCommand, Response>
{
    private readonly IAuthorizationApiProxy _authorizationApiProxy;
    private readonly IEmailServices _emailServices;
    private readonly IReceptionistRepository _receptionistRepository;
    private readonly UserManager<Account> _userManager;

    public AddReceptionistRoleCommandHandler(UserManager<Account> userManager, IReceptionistRepository receptionist,
        IAuthorizationApiProxy authorizationService, IEmailServices emailServices)
    {
        _userManager = userManager;
        _authorizationApiProxy = authorizationService;
        _receptionistRepository = receptionist;
        _emailServices = emailServices;
    }

    public async Task<Response> Handle(AddReceptionistRoleCommand request, CancellationToken cancellationToken)
    {
        var role = UserRoles.Receptionist;
        var password = Guid.NewGuid().ToString().Substring(0, 8);
        await _emailServices.SendEmailAsync(request.ReceptionistDTO.Email, "Credentials Hospital",
            $"Log in to your account using the credentials below: \n email: {request.ReceptionistDTO.Email} \n password: {password}",
            cancellationToken);
        var accountId = await _authorizationApiProxy.RegistrationAsync(new UserDTO()
        {
            BirthDate = request.ReceptionistDTO.BirthDate,
            Email = request.ReceptionistDTO.Email,
            FirstName = request.ReceptionistDTO.FirstName,
            LastName = request.ReceptionistDTO.LastName,
            MiddleName = request.ReceptionistDTO.MiddleName,
            Password = password,
            PhoneNumber = request.ReceptionistDTO.PhoneNumber
        }, cancellationToken);
        var user = await _userManager.FindByIdAsync(accountId);
        if (user != null)
        {
            await _userManager.AddToRoleAsync(user, role);
            await _receptionistRepository.InsertAsync(new Receptionist
            {
                AccountId = user.Id,
                OfficeId = request.ReceptionistDTO.OfficeId
            }, cancellationToken);
            return Response.Success;
        }

        return Response.Error;
    }
}