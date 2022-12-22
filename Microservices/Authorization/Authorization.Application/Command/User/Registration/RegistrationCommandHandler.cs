using Authorization.Application.Contracts.Incoming.User;
using Authorization.Application.Contracts.Outgoing;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Application.Command.User.Registration
{
    public class RegistrationCommandHandler:IRequestHandler<RegistrationCommand,string>
    {
        private readonly UserManager<Account> _userManager;
        public RegistrationCommandHandler(UserManager<Account> userManager)
        {
            _userManager = userManager;
        }

       public async Task<string> Handle(RegistrationCommand request, CancellationToken cancellationToken)
       {
           var appUser = new Account()
           {
               FirstName = request.User.FirstName,
               LastName = request.User.LastName,
               MiddleName = request.User.MiddleName,
               Email = request.User.Email,
               PhoneNumber = request.User.PhoneNumber,
               UserName = request.User.Login
           };
           var result =  await _userManager.CreateAsync(appUser, request.User.Password);

           if (result.Succeeded)
           {
               return appUser.Id;
           }

           throw new Exception(string.Join("/n", result.Errors.Select(x => x.Description)));
       }
    }
}
