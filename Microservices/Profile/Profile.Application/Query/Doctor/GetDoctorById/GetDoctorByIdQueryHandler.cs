using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Doctor.GetDoctorById
{
    public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, DoctorOneDTO>
    {
        private readonly IDoctorRepository _doctorRepository;

        public GetDoctorByIdQueryHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public async Task<DoctorOneDTO> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
          return await _doctorRepository.GetDoctorByIdAsync(request.DoctorId,cancellationToken);
        }
    }
}
