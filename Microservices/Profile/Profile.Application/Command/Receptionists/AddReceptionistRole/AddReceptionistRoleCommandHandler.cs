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
        private readonly IEmailServices _emailServices;
        public AddReceptionistRoleCommandHandler(UserManager<Account> userManager, IReceptionistRepository receptionist, IAuthorizationService authorizationService, IEmailServices emailServices)
        {
            _userManager = userManager;
            _authorizationService = authorizationService;
            _receptionistRepository = receptionist;
            _emailServices = emailServices;
        }

        public async Task<Response> Handle(AddReceptionistRoleCommand request, CancellationToken cancellationToken)
        {
            var role = UserRoles.Receptionist;
            var password = Guid.NewGuid().ToString().Substring(0, 8);
            await _emailServices.SendEmailAsync(request.ReceptionistDTO.Email, "Credentials Hospital",
                $"Log in to your account using the credentials below: \n email: { request.ReceptionistDTO.Email} \n password: {password}" , cancellationToken);
            var AccountId= await _authorizationService.SendReceptionistInfoForRegistrationAsync(request.ReceptionistDTO,password,
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
