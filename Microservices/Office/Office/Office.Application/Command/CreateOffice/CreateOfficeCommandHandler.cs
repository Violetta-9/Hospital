using Authorization.Data.Repository;
using MediatR;
using OfficeEntity = Authorization.Data_Domain.Models.Office;

namespace Office.Application.Command.CreateOffice;

internal class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, long>
{
    private readonly IOfficeRepository _officeRepository;

    public CreateOfficeCommandHandler(IOfficeRepository officeRepository)
    {
        _officeRepository = officeRepository;
    }

    public async Task<long> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
    {
        var office = new OfficeEntity
        {
            Address = request.OfficeDto.Address,
            RegistryPhoneNumber = request.OfficeDto.RegistryPhoneNumber,
            IsActive = true,
        };
        await _officeRepository.InsertAsync(office, cancellationToken);
        return office.Id;
    }
}