using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;

namespace Profile.Application.Command.Receptionists.DeleteDoctor
{
    public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, Response>
    {
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDoctorRepository _doctorRepository;
        public DeleteDoctorCommandHandler(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager, IDoctorRepository doctorRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _doctorRepository = doctorRepository;
        }
        public async Task<Response> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var role = UserRoles.Doctor;
            var doctor =await _doctorRepository.GetDoctorByAccountIdAsync(request.AccountId, cancellationToken);
            if (doctor == null)
            {
                return Response.Error;
            }

            await _doctorRepository.DeleteAsync(doctor.Id, cancellationToken);
            var user = await _userManager.FindByIdAsync(request.AccountId);
            if (user == null)
            {
                return Response.Error;
            }
            await _userManager.RemoveFromRoleAsync(user, role);
            return Response.Success;

        }
    }
}
