using System.Text;
using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Profile.Application.Command.Receptionists.AddDoctorRole;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;
using Profile.Application.Services;

namespace Profile.Application.Command.Receptionists.AddDoctorRole
{
    public class AddDoctorRoleCommandHandler : IRequestHandler<AddDoctorRoleCommand, Response>
    {

        private readonly UserManager<Account> _userManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IDoctorRepository _doctorRepository;
        public AddDoctorRoleCommandHandler(UserManager<Account> userManager,IDoctorRepository doctorRepository, IAuthorizationService authorization)
        {
            _userManager = userManager;
            _authorizationService = authorization;
            _doctorRepository = doctorRepository;
        }

        public async Task<Response> Handle(AddDoctorRoleCommand request, CancellationToken cancellationToken)
        {
            //todo: email
            var role = UserRoles.Doctor;
            var AccountId=await _authorizationService.SendDoctorInfoForRegistrationAsync(request.Doctor, cancellationToken);
            var user = await _userManager.FindByIdAsync(AccountId);
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
