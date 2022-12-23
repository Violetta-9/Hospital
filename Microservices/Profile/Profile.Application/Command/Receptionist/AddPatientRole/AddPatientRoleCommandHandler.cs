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

namespace Profile.Application.Command.Receptionist.AddPatientRole
{
    public class AddPatientRoleCommandHandler:IRequestHandler<AddPatientRoleCommand,Response>
    {
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AddPatientRoleCommandHandler(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Response> Handle(AddPatientRoleCommand request, CancellationToken cancellationToken)
        {
            var role = UserRoles.Patient;
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
