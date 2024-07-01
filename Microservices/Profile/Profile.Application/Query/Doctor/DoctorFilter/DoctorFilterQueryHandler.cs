using Authorization.Data.Repository;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Doctor.DoctorFilter
{
    internal class DoctorFilterQueryHandler : IRequestHandler<DoctorFilterQuery, DoctorAllDTO[]>
    {
        private readonly IDoctorRepository _doctorRepository;
        public DoctorFilterQueryHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public async Task<DoctorAllDTO[]> Handle(DoctorFilterQuery request, CancellationToken cancellationToken)
        {
            var resultList = new List<DoctorAllDTO>();
            var firstName = request.DoctorFilter.FirstName ?? "";
            var lastName = request.DoctorFilter.LastName ?? "";
            var middleName = request.DoctorFilter.MiddleName ?? "";
            if (firstName != "" || lastName != "" || middleName != "")
            {
                resultList.AddRange(await _doctorRepository.FindDoctorByFullNameAsync(firstName, lastName, middleName,
                    cancellationToken));
            }
            else if (request.DoctorFilter.OfficeId.HasValue)
            {
                 resultList.AddRange(await _doctorRepository.GetDoctorByOfficeIdAsync(request.DoctorFilter.OfficeId.Value, cancellationToken));
            }
            else if (request.DoctorFilter.SpecializationId.HasValue)
            {
                resultList.AddRange(await _doctorRepository.GetDoctorBySpecializationIdAsync(request.DoctorFilter.SpecializationId.Value, cancellationToken));
            }
            else
            {
                resultList.AddRange(await _doctorRepository.GetAllDoctorsAsync(cancellationToken));
            }

            return resultList.ToArray();

        }
    }
}
