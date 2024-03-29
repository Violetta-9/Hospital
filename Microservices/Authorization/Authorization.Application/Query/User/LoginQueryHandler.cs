﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authorization.Application.Helpers;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.Application.Query.User;

public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
{
    private readonly JwtSettings _jwtSettings;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<Account> _userManager;

    public LoginQueryHandler(UserManager<Account> userManager, IOptions<JwtSettings> jwtSettings,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _roleManager = roleManager;
    }

    public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (!user.EmailConfirmed) throw new Exception("Email not confirmed");
        var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);

        var listOfClaims = await CreateClaim(user);

        if (checkPassword)
        {
            var a = _jwtSettings.JwtSecretKey;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(listOfClaims),
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

        throw new Exception("Email or password are wrong");
    }

    private async Task<List<Claim>> CreateClaim(Account user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var options = new IdentityOptions();
        var listOfClaims = roles.Select(role => new Claim(options.ClaimsIdentity.RoleClaimType, role)).ToList();
        listOfClaims.Add(new Claim(options.ClaimsIdentity.UserIdClaimType, user.Id));
        listOfClaims.Add(new Claim(JwtRegisteredClaimNames.Name, user.FirstName));
        listOfClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        return listOfClaims;
    }
}