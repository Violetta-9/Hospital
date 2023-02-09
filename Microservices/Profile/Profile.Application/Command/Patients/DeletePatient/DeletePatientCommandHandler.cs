using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using Documents.API.Client.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;

namespace Profile.Application.Command.Patients.DeletePatient;

internal class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, Response>
{
    private readonly IDocumentApiProxy _documentApiProxy;
    private readonly IPatientRepository _patientRepository;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<Account> _userManager;

    public DeletePatientCommandHandler(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager,
        IPatientRepository patientRepository, IDocumentApiProxy documentApiProxy)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _patientRepository = patientRepository;
        _documentApiProxy = documentApiProxy;
    }

    public async Task<Response> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        var role = UserRoles.Patient;
        var patient = _patientRepository.GetPatientByAccountId(request.AccountId, cancellationToken);
        if (patient == null) return Response.Error;

        await _patientRepository.DeleteAsync(patient.Id, cancellationToken);
        var user = await _userManager.FindByIdAsync(request.AccountId);
        if (user == null) return Response.Error;

        await _userManager.RemoveFromRoleAsync(user, role);

        if (user.PhotoId != null)
        {
            var documentId = (long)user.PhotoId;
            user.PhotoId = null;
            await _userManager.UpdateAsync(user);
            await _documentApiProxy.DeleteBlobAsync(documentId, cancellationToken);
        }

        return Response.Success;
    }
}