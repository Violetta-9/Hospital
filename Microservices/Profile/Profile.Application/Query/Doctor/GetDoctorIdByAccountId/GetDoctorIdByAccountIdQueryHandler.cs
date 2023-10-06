using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;

namespace Profile.Application.Query.Doctor.GetDoctorIdByAccountId
{
    internal class GetDoctorIdByAccountIdQueryHandler : IRequestHandler<GetDoctorIdByAccountIdQuery, long>
    {
        private readonly IDoctorRepository _doctorRepository;
        public GetDoctorIdByAccountIdQueryHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<long> Handle(GetDoctorIdByAccountIdQuery request, CancellationToken cancellationToken)
        {
          return  await _doctorRepository.GetDoctorIdByAccountIdAsync(request.AccountId,cancellationToken);
        }
    }
}
