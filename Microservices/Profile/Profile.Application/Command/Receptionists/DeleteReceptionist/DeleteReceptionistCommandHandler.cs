using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;

namespace Profile.Application.Command.Receptionists.DeleteReceptionist
{
    internal class DeleteReceptionistCommandHandler : IRequestHandler<DeleteReceptionistCommand, Response>
    {
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IReceptionistRepository _receptionistRepository;
        public DeleteReceptionistCommandHandler(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager,IReceptionistRepository receptionistRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
           _receptionistRepository= receptionistRepository;
        }
        public async Task<Response> Handle(DeleteReceptionistCommand request, CancellationToken cancellationToken)
        {
            var role = UserRoles.Receptionist;
            var receptionist = await _receptionistRepository.GetReceptionistByAccountIdAsync(request.AccountId, cancellationToken);
            if (receptionist == null)
            {
                return Response.Error;
            }

            await _receptionistRepository.DeleteAsync(receptionist.Id, cancellationToken);
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
