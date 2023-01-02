﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Update.UpdateAccountIfo
{
    public class UpdateAccountInfoCommandHandler : IRequestHandler<UpdateAccountInfoCommand, Response>
    {
        private readonly UserManager<Account> _userManager;

        public UpdateAccountInfoCommandHandler(UserManager<Account> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Response> Handle(UpdateAccountInfoCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserDtO.AccountId);
            if (user == null)
            {
                return Response.Error;
            }

            user.FirstName=request.UserDtO.FirstName;
            user.LastName=request.UserDtO.LastName;
            user.Email=request.UserDtO.Email;
            user.PhoneNumber = request.UserDtO.PhoneNumber;
            user.Birthday = new DateTime(request.UserDtO.Year, request.UserDtO.Month, request.UserDtO.Day);
            await _userManager.UpdateAsync(user);
            return Response.Success;
        }
    }
}
