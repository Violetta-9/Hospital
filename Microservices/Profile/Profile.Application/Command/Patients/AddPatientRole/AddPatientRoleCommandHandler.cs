using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;

namespace Profile.Application.Command.Patients.AddPatientRole;

public class AddPatientRoleCommandHandler : IRequestHandler<AddPatientRoleCommand, Response>
{
    private readonly IPatientRepository _patientRepository;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<Account> _userManager;

    public AddPatientRoleCommandHandler(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager,
        IPatientRepository patientRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _patientRepository = patientRepository;
    }

    public async Task<Response> Handle(AddPatientRoleCommand request, CancellationToken cancellationToken)
    {
        var role = UserRoles.Patient;
        var user = await _userManager.FindByIdAsync(request.AccountId);
        if (user != null)
        {
            await _userManager.AddToRoleAsync(user, role);
            await _patientRepository.InsertAsync(new Patient
            {
                AccountId = user.Id
            }, cancellationToken);

            return Response.Success;
        }

        return Response.Error;
    }
}