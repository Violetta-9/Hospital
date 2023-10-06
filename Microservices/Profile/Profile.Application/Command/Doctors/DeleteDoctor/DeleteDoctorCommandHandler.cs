using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using Documents.API.Client.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;

namespace Profile.Application.Command.Doctors.DeleteDoctor;

public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, Response>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IDocumentApiProxy _documentApiProxy;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<Account> _userManager;

    public DeleteDoctorCommandHandler(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager,
        IDoctorRepository doctorRepository, IDocumentApiProxy documentApiProxy)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _doctorRepository = doctorRepository;
        _documentApiProxy = documentApiProxy;
    }

    public async Task<Response> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var role = UserRoles.Doctor;
        var doctorId = await _doctorRepository.GetDoctorIdByAccountIdAsync(request.AccountId, cancellationToken);
        if (doctorId == null) return Response.Error;

        await _doctorRepository.DeleteAsync(doctorId, cancellationToken);
        var user = await _userManager.FindByIdAsync(request.AccountId);
        if (user == null) return Response.Error;
        await _userManager.RemoveFromRoleAsync(user, role);

        if (user.PhotoId != null)
        {
            var documentationId = (long)user.PhotoId;
            user.PhotoId = null;
            await _userManager.UpdateAsync(user);
            await _documentApiProxy.DeleteBlobAsync(documentationId, cancellationToken);
        }

        return Response.Success;
    }
}