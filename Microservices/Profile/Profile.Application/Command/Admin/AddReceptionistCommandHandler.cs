using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;

namespace Profile.Application.Command.Admin
{
    public class AddReceptionistCommandHandler : IRequestHandler<AddReceptionistRoleCommand, Response>
    {
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AddReceptionistCommandHandler(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Response> Handle(AddReceptionistRoleCommand request, CancellationToken cancellationToken)
        {
            var role = UserRoles.Receptionist;
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user!= null )
            {
               await _userManager.AddToRoleAsync(user,role);
               return Response.Success;
            }
            return Response.Error;
        }
    }
}
