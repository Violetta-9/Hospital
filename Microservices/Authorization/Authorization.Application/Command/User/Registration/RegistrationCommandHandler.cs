using Authorization.Application.Helpers;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Application.Command.User.Registration
{
    public class RegistrationCommandHandler:IRequestHandler<RegistrationCommand,string>
    {
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegistrationCommandHandler(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var role = UserRoles.User;

           var appUser = new Account()
           {
               
               FirstName = request.User.FirstName,
               LastName = request.User.LastName,
               MiddleName = request.User.MiddleName,
               Email = request.User.Email,
               PhoneNumber = request.User.PhoneNumber,
               Birthday = request.User.BirthDate
           };
           if ( !await _roleManager.RoleExistsAsync(role))
           {
               var newRole=new IdentityRole(role);
               await _roleManager.CreateAsync(newRole);
           }
           var result =  await _userManager.CreateAsync(appUser, request.User.Password);
           await _userManager.AddToRoleAsync(appUser, role);

           if (result.Succeeded)
           {
               return appUser.Id;
           }

           throw new Exception(string.Join("/n", result.Errors.Select(x => x.Description)));
       }
    }
}
