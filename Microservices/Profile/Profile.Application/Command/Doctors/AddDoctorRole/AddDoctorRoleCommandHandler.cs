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

namespace Profile.Application.Command.Doctors.AddDoctorRole;

public class AddDoctorRoleCommandHandler : IRequestHandler<AddDoctorRoleCommand, Response>
{
    private readonly IAuthorizationApiProxy _authorizationApiProxy;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IDocumentApiProxy _documentApiProxy;
    private readonly IEmailServices _emailServices;

    private readonly UserManager<Account> _userManager;

    public AddDoctorRoleCommandHandler(UserManager<Account> userManager, IDoctorRepository doctorRepository,
        IAuthorizationApiProxy authorization, IEmailServices emailServices, IDocumentApiProxy documentApiProxy)
    {
        _userManager = userManager;
        _authorizationApiProxy = authorization;
        _doctorRepository = doctorRepository;
        _emailServices = emailServices;
        _documentApiProxy = documentApiProxy;
    }

    public async Task<Response> Handle(AddDoctorRoleCommand request, CancellationToken cancellationToken)
    {
        //todo: email

        var role = UserRoles.Doctor;
        var password = Guid.NewGuid().ToString().Substring(0, 8);
        var accountId = await _authorizationApiProxy.RegistrationAsync(new UserDTO
        {
            BirthDate = request.Doctor.BirthDate,
            Email = request.Doctor.Email,
            FirstName = request.Doctor.FirstName,
            LastName = request.Doctor.LastName,
            MiddleName = request.Doctor.MiddleName,
            Password = password,
            PhoneNumber = request.Doctor.PhoneNumber
        }, cancellationToken);

        await _emailServices.SendEmailAsync(request.Doctor.Email, "Credentials Hospital",
            $"Log in to your account using the credentials below: \n email: {request.Doctor.Email} \n password: {password}",
            cancellationToken);


        var user = await _userManager.FindByIdAsync(accountId);
        if (user != null)
        {
            await _userManager.AddToRoleAsync(user, role);
            var doc = await _doctorRepository.InsertAsync(new Doctor
            {
                AccountId = user.Id,
                CareerStartYear = DateTime.Now,
                SpecializationId = request.Doctor.SpecializationId,
                OfficeId = request.Doctor.OfficeId,
                StatusId = request.Doctor.StatusId
            }, cancellationToken);


            if (request.Doctor.File != null)
            {
                var response = await _documentApiProxy.UploadBlobAsync(
                    new FileParameter(request.Doctor.File.OpenReadStream(), request.Doctor.File.FileName,
                        request.Doctor.File.ContentType), doc.Id, SubjectUpdate._0, cancellationToken);
                if (response > 0)
                {
                    user.PhotoId = response;
                    await _userManager.UpdateAsync(user);
                }
            }

            return Response.Success;
        }

        return Response.Error;
    }
}