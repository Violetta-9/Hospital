using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Command.Admin;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;

namespace Profile.Application.Command.Receptionist.AddDoctorRole
{
    public class AddDoctorRoleCommandHandler : IRequestHandler<AddDoctorRoleCommand, Response>
    {
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AddDoctorRoleCommandHandler(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Response> Handle(AddDoctorRoleCommand request, CancellationToken cancellationToken)
        {
            var role = UserRoles.Doctor;
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, role);
                return Response.Success;
            }
            return Response.Error;
        }

    }
}
