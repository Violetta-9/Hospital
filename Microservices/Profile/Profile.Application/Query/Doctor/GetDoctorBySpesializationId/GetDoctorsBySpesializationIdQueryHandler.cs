using Authorization.Data.Repository;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Query.Doctor.GetDoctorByOfficeId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Profile.Application.Query.Doctor.GetDoctorBySpesializationId
{
    internal class GetDoctorsBySpesializationIdQueryHandler : IRequestHandler<GetDoctorsBySpesializationIdQuery, DoctorAllDTO[]>
    {
        private readonly IDoctorRepository _doctorRepository;

        public GetDoctorsBySpesializationIdQueryHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public async Task<DoctorAllDTO[]> Handle(GetDoctorsBySpesializationIdQuery request, CancellationToken cancellationToken)
        {
            return await _doctorRepository.GetDoctorBySpecializationIdAsync(request.SpesializationId, cancellationToken);
        }
    }
}
