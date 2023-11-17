using MediatR;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Query.Doctor.GetAllDoctors;
using Profile.Application.Query.Doctor.GetDoctorByFullName;
using Profile.Application.Query.Doctor.GetDoctorByOfficeId;
using Profile.Application.Query.Doctor.GetDoctorBySpesializationId;

namespace Profile.Application.Query.Doctor.DoctorFilter
{
    internal class DoctorFilterQueryHandler : IRequestHandler<DoctorFilterQuery, DoctorAllDTO[]>
    {
        private readonly IMediator _mediator;
        private DoctorAllDTO[] Response;
        public DoctorFilterQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
            Response = new DoctorAllDTO[0];
        }
        public async Task<DoctorAllDTO[]> Handle(DoctorFilterQuery request, CancellationToken cancellationToken)
        {
            var firstName = (request.DoctorFilter.FirstName != null) ? request.DoctorFilter.FirstName : "";
            var lastName = (request.DoctorFilter.LastName != null) ? request.DoctorFilter.LastName : "";
            var middleName = (request.DoctorFilter.MiddleName != null) ? request.DoctorFilter.MiddleName : "";
            if (firstName != "" || lastName != "" || middleName != "")
            {
                Response = await _mediator.Send(new GetDoctorByFullNameQuery(new DoctorsFullNameDTO()
                {
                    FirstName =firstName,
                    LastName = lastName,
                    MiddleName = middleName
                }));
            }
            else if (request.DoctorFilter.OfficeId != null)
            {
                var response = await _mediator.Send(new GetDoctorsByOfficeIdQuery((long)request.DoctorFilter.OfficeId));
                Array.Resize(ref Response, Response.Length + response.Length); // увеличение размера массива array1 на длину массива array2

                Array.Copy(response, 0, Response, Response.Length - response.Length, response.Length);
            }
            else if (request.DoctorFilter.SpecializationId != null)
            {
                var response = await _mediator.Send(new GetDoctorsBySpesializationIdQuery((long)request.DoctorFilter.SpecializationId));
                Array.Resize(ref Response, Response.Length + response.Length);

                Array.Copy(response, 0, Response, Response.Length - response.Length, response.Length);
            }
            else
            {
                return await _mediator.Send(new GetAllDoctorsQuery());
            }

            return Response;

        }
    }
}
