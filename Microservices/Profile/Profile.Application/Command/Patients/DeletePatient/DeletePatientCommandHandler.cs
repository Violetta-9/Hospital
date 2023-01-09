using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;

namespace Profile.Application.Command.Patients.DeletePatient;

internal class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, Response>
{
    private readonly IPatientRepository _patientRepository;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<Account> _userManager;

    public DeletePatientCommandHandler(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager,
        IPatientRepository patientRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _patientRepository = patientRepository;
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
        return Response.Success;
    }
}