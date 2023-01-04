using System.Text;
using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;
using Profile.Application.Services;

namespace Profile.Application.Command.Doctors.AddDoctorRole
{
    public class AddDoctorRoleCommandHandler : IRequestHandler<AddDoctorRoleCommand, Response>
    {

        private readonly UserManager<Account> _userManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IEmailServices _emailServices;
        private readonly IDoctorRepository _doctorRepository;
        public AddDoctorRoleCommandHandler(UserManager<Account> userManager,IDoctorRepository doctorRepository, IAuthorizationService authorization, IEmailServices emailServices)
        {
            _userManager = userManager;
            _authorizationService = authorization;
            _doctorRepository = doctorRepository;
            _emailServices = emailServices;
        }

        public async Task<Response> Handle(AddDoctorRoleCommand request, CancellationToken cancellationToken)
        {
            //todo: email

            var role = UserRoles.Doctor;
            var password = Guid.NewGuid().ToString().Substring(0, 8);
            var accountId=await _authorizationService.SendDoctorInfoForRegistrationAsync(request.Doctor,password, cancellationToken);

            await _emailServices.SendEmailAsync(request.Doctor.Email, "Credentials Hospital",
                $"Log in to your account using the credentials below: \n email: {request.Doctor.Email} \n password: {password}",
                cancellationToken);
            var user = await _userManager.FindByIdAsync(accountId);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, role);
                await _doctorRepository.InsertAsync(new Doctor()
                {
                    AccountId = user.Id,
                    CareerStartYear =DateTime.Now,
                    SpecializationId = request.Doctor.SpecializationId,
                    OfficeId = request.Doctor.OfficeId,
                    StatusId = request.Doctor.StatusId,


                }, cancellationToken);
                return Response.Success;
            }
            return Response.Error;
        }

    }
}
