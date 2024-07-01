using Authorization.API.Client.Abstraction;
using Authorization.API.Client.GeneratedClient;
using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using Documents.API.Client.Abstraction;
using Documents.API.Client.GeneratedClient;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Helpers;
using Profile.Application.Services;
using Response = Profile.Application.Contracts.Outgoing.Response;

namespace Profile.Application.Command.Receptionists.AddReceptionistRole;

public class AddReceptionistRoleCommandHandler : IRequestHandler<AddReceptionistRoleCommand, Response>
{
    private readonly IAuthorizationApiProxy _authorizationApiProxy;
    private readonly IDocumentApiProxy _documentApiProxy;
    private readonly IEmailServices _emailServices;
    private readonly IReceptionistRepository _receptionistRepository;
    private readonly UserManager<Account> _userManager;


    public AddReceptionistRoleCommandHandler(UserManager<Account> userManager, IReceptionistRepository receptionist,
        IAuthorizationApiProxy authorizationService, IEmailServices emailServices, IDocumentApiProxy documentApiProxy)
    {
        _userManager = userManager;
        _authorizationApiProxy = authorizationService;
        _receptionistRepository = receptionist;
        _emailServices = emailServices;
        _documentApiProxy = documentApiProxy;
    }

    public async Task<Response> Handle(AddReceptionistRoleCommand request, CancellationToken cancellationToken)
    {
        var role = UserRoles.Receptionist;
        var password = Guid.NewGuid().ToString().Substring(0, 8);
        await _emailServices.SendEmailAsync(request.ReceptionistDTO.Email, "Credentials Hospital",
            $"Log in to your account using the credentials below: \n email: {request.ReceptionistDTO.Email} \n password: {password}",
            cancellationToken);
        var accountId = await _authorizationApiProxy.RegistrationAsync(new UserDTO
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
            var recep = await _receptionistRepository.InsertAsync(new Receptionist
            {
                AccountId = user.Id,
                OfficeId = request.ReceptionistDTO.OfficeId
            }, cancellationToken);

            if (request.ReceptionistDTO.File != null)
            {
                var response = await _documentApiProxy.UploadBlobAsync(
                    new FileParameter(request.ReceptionistDTO.File.OpenReadStream(),
                        request.ReceptionistDTO.File.FileName,
                        request.ReceptionistDTO.File.ContentType), recep.Id,0, SubjectUpdate.Receptionist, cancellationToken);
                if (response > 0)
                {
                    user.PhotoId = response;
                    await _userManager.UpdateAsync(user);
                    return Response.Success;
                }

                return Response.Error;
            }

            return Response.Success;
        }

        return Response.Error;
    }
}