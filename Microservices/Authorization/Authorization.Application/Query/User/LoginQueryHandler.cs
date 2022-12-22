﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Authorization.Application.Helpers;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.Application.Query.User
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly UserManager<Account> _userManager;
        private readonly JwtSettings _jwtSettings;
        public LoginQueryHandler(UserManager<Account> userManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (checkPassword)
            {
                var a = _jwtSettings.JwtSecretKey;
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", user.Id),
                        new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti,user.Id)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(20),
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.JwtSecretKey)),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return stringToken;

            }

            throw new Exception("Что-то пошло не так");




        }
    }
}
