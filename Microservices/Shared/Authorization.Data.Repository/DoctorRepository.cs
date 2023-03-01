using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;

namespace Authorization.Data.Repository;

public interface IDoctorRepository : IRepositoryBase<Doctor>
{
    Task<Doctor> GetDoctorByAccountIdAsync(string accountId,
        CancellationToken cancellationToken = default);

    public Task<DoctorAllDTO[]> GetAllDoctorsAsync(CancellationToken cancellationToken = default);
    public Task<DoctorOneDTO?> GetDoctorByIdAsync(long id, CancellationToken cancellationToken = default);

    public Task<DoctorAllDTO[]> GetDoctorByOfficeIdAsync(long officeid,
        CancellationToken cancellationToken = default);

    public Task<DoctorAllDTO[]> GetDoctorBySpecializationIdAsync(long spesialization,
        CancellationToken cancellationToken = default);

    public Task<DoctorAllDTO[]> FindDoctorByFullNameAsync(string firsName, string lastName, string middleName,
        CancellationToken cancellationToken = default);
}

internal class DoctorRepository : RepositoryBase<Doctor>, IDoctorRepository
{
    private readonly BlobUrlHelpers _blobUrlHelpers;

    public DoctorRepository(HospitalDbContext dbContext, IOptions<BlobUrlHelpers> blobOptions) : base(dbContext)
    {
        _blobUrlHelpers = blobOptions.Value;
    }

    public async Task<Doctor> GetDoctorByAccountIdAsync(string accountId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Doctors.SingleOrDefaultAsync(x => x.AccountId == accountId, cancellationToken);
    }

    public async Task<DoctorAllDTO[]> GetAllDoctorsAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Doctors.Select(x => new DoctorAllDTO
        {
            Id = x.Id,
            FirstName = x.Account.FirstName,
            LastName = x.Account.LastName,
            MiddleName = x.Account.MiddleName,
            BirthDay = x.Account.Birthday,
            OfficeAddress = x.Office.Address,
            SpecializationTitle = x.Specialization.Title,
            StatusTitle = x.Status.Title,
            CareerDate = x.CareerStartYear,
            DocumentAbsolutUrl = _blobUrlHelpers.AbsolutUrl + x.Account.Photo.Path
        }).ToArrayAsync(cancellationToken);
    }

    public async Task<DoctorOneDTO?> GetDoctorByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Doctors.Where(x => x.Id == id).Select(x => new DoctorOneDTO
        {
            AccountId = x.AccountId,
            FirstName = x.Account.FirstName,
            LastName = x.Account.LastName,
            MiddleName = x.Account.MiddleName,
            BirthDay = x.Account.Birthday,
            PhoneNumber = x.Account.PhoneNumber,
            OfficeAddress = x.Office.Address,
            SpecializationTitle = x.Specialization.Title,
            StatusTitle = x.Status.Title,
            CareerStartYear = x.CareerStartYear,
            DocumentAbsolutUrl = _blobUrlHelpers.AbsolutUrl + x.Account.Photo.Path
        }).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<DoctorAllDTO[]> GetDoctorByOfficeIdAsync(long officeid,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Doctors.Where(x => x.OfficeId == officeid).Select(x => new DoctorAllDTO
        {
            Id = x.Id,
            AccountId = x.AccountId,
            FirstName = x.Account.FirstName,
            LastName = x.Account.LastName,
            MiddleName = x.Account.MiddleName,
            BirthDay = x.Account.Birthday,
            OfficeAddress = x.Office.Address,
            SpecializationTitle = x.Specialization.Title,
            StatusTitle = x.Status.Title,
            DocumentAbsolutUrl = _blobUrlHelpers.AbsolutUrl + x.Account.Photo.Path
        }).ToArrayAsync(cancellationToken);
    }

    public async Task<DoctorAllDTO[]> GetDoctorBySpecializationIdAsync(long spesialization,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Doctors.Where(x => x.SpecializationId == spesialization).Select(x => new DoctorAllDTO
        {
            Id = x.Id,
            AccountId = x.AccountId,
            FirstName = x.Account.FirstName,
            LastName = x.Account.LastName,
            MiddleName = x.Account.MiddleName,
            BirthDay = x.Account.Birthday,
            OfficeAddress = x.Office.Address,
            SpecializationTitle = x.Specialization.Title,
            StatusTitle = x.Status.Title,
            DocumentAbsolutUrl = _blobUrlHelpers.AbsolutUrl + x.Account.Photo.Path
        }).ToArrayAsync(cancellationToken);
    }

    public async Task<DoctorAllDTO[]> FindDoctorByFullNameAsync(string firsName, string lastName, string middleName,
        CancellationToken cancellationToken = default)
    {
        var lowerFirstNameTerm = firsName.ToLower();
        var lowerLastNameTerm = lastName.ToLower();
        var lowerMiddleNameTerm = middleName.ToLower();
        var matchExpressionFirstName = $"%{lowerFirstNameTerm}%";
        var matchExpressionLastName = $"%{lowerLastNameTerm}%";
        var matchExpressionMiddleName = $"%{lowerMiddleNameTerm}%";
        var doctors = await DbContext.Doctors.Where(x =>
            EF.Functions.Like(x.Account.FirstName.ToLower(), matchExpressionFirstName) ||
            EF.Functions.Like(x.Account.LastName.ToLower(), matchExpressionLastName) ||
            EF.Functions.Like(x.Account.MiddleName.ToLower(), matchExpressionMiddleName)
        ).Select(x => new DoctorAllDTO
        {
            Id = x.Id,
            FirstName = x.Account.FirstName,
            LastName = x.Account.LastName,
            MiddleName = x.Account.MiddleName,
            BirthDay = x.Account.Birthday,
            OfficeAddress = x.Office.Address,
            SpecializationTitle = x.Specialization.Title,
            StatusTitle = x.Status.Title,
            DocumentAbsolutUrl = _blobUrlHelpers.AbsolutUrl + x.Account.Photo.Path
        }).ToArrayAsync(cancellationToken);
        return doctors;
    }
}