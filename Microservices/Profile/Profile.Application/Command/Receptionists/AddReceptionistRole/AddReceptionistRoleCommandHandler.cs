using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;
using Profile.Application.Services;

namespace Profile.Application.Command.Receptionists.AddReceptionistRole
{
    public class AddReceptionistRoleCommandHandler : IRequestHandler<AddReceptionistRoleCommand, Response>
    {
        private readonly UserManager<Account> _userManager;
       private readonly IAuthorizationService _authorizationService;
        private readonly IReceptionistRepository _receptionistRepository;
        public AddReceptionistRoleCommandHandler(UserManager<Account> userManager, IReceptionistRepository receptionist, IAuthorizationService authorizationService)
        {
            _userManager = userManager;
          _authorizationService=authorizationService;
            _receptionistRepository = receptionist;
        }

        public async Task<Response> Handle(AddReceptionistRoleCommand request, CancellationToken cancellationToken)
        {
            var role = UserRoles.Receptionist;
           var AccountId= await _authorizationService.SendReceptionistInfoForRegistrationAsync(request.ReceptionistDTO,
                cancellationToken);
            var user = await _userManager.FindByIdAsync(AccountId);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, role);
                await _receptionistRepository.InsertAsync(new Receptionist()
                {
                    AccountId = user.Id,
                    OfficeId = request.ReceptionistDTO.OfficeId,

                }, cancellationToken);
                return Response.Success;
            }
            return Response.Error;
        }
    }
}
