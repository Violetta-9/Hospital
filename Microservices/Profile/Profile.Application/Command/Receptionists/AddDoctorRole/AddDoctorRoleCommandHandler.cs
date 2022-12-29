using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Command.Receptionists.AddDoctorRole;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;

namespace Profile.Application.Command.Receptionists.AddDoctorRole
{
    public class AddDoctorRoleCommandHandler : IRequestHandler<AddDoctorRoleCommand, Response>
    {

        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDoctorRepository _doctorRepository;
        public AddDoctorRoleCommandHandler(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager,IDoctorRepository doctorRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _doctorRepository = doctorRepository;
        }

        public async Task<Response> Handle(AddDoctorRoleCommand request, CancellationToken cancellationToken)
        {
            //todo:
            var role = UserRoles.Doctor;
            var user = await _userManager.FindByIdAsync(request.Doctor.AccountId);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, role);
                await _doctorRepository.InsertAsync(new Doctor()
                {
                    AccountId = user.Id,
                    CareerStartYear =DateTime.Now,
                    SpecializationId = request.Doctor.SpecializationId,
                    OfficeId = request.Doctor.OfficeId,
                    StatusId = request.Doctor.StatusId,


                }, cancellationToken);
                return Response.Success;
            }
            return Response.Error;
        }

    }
}
