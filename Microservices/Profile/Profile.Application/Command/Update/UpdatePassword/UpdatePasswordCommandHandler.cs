using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Update.UpdatePassword
{
    internal class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Response>
    {
        private readonly UserManager<Account> _userManager;

        public UpdatePasswordCommandHandler(UserManager<Account> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Response> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.NewPassword.AccountId);
            if (user == null)
            {
                return Response.Error;
            }
            await _userManager.ChangePasswordAsync(user, request.NewPassword.OldPassword,
                    request.NewPassword.NewPassword);
            return Response.Success;
            
        }
    }
}
