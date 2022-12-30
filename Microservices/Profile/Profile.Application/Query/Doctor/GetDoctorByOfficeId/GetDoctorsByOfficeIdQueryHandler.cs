using Authorization.Data.Repository;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Query.Doctor.GetDoctorById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Profile.Application.Query.Doctor.GetDoctorByOfficeId
{
    internal class GetDoctorsByOfficeIdQueryHandler : IRequestHandler<GetDoctorsByOfficeIdQuery, DoctorAllDTO[]>
    {
        private readonly IDoctorRepository _doctorRepository;

        public GetDoctorsByOfficeIdQueryHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public async Task<DoctorAllDTO[]> Handle(GetDoctorsByOfficeIdQuery request, CancellationToken cancellationToken)
        {
            return await _doctorRepository.GetDoctorByOfficeIdAsync(request.OfficeId, cancellationToken);
        }
    }
}
