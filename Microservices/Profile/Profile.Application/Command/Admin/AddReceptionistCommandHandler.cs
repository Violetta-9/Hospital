using Authorization.Data.Repository;
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
        private readonly IReceptionistRepository _receptionistRepository;
        public AddReceptionistCommandHandler(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager, IReceptionistRepository receptionist)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _receptionistRepository = receptionist;
        }

        public async Task<Response> Handle(AddReceptionistRoleCommand request, CancellationToken cancellationToken)
        {
            var role = UserRoles.Receptionist;
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user!= null )
            {
               await _userManager.AddToRoleAsync(user,role);
               await _receptionistRepository.InsertAsync(new Receptionist()
               {
                   AccountId = user.Id,
                   OfficeId = request.OfficeId,

               }, cancellationToken);
               return Response.Success;
            }
            return Response.Error;
        }
    }
}
