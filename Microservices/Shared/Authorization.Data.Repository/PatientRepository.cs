using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Profile.Application.Contracts.Outgoing;
using Profile.Application.Helpers;

namespace Authorization.Data.Repository;

public interface IPatientRepository : IRepositoryBase<Patient>
{
    Task<long> GetPatientIdByAccountId(string accountId,
        CancellationToken cancellationToken = default);

    public Task<PatientAllDTO[]> GetAllPatientsAsync(CancellationToken cancellationToken = default);
    public Task<PatientOneDTO?> GetPatientByIdAsync(long id, CancellationToken cancellationToken = default);

    public Task<UsersDTO[]> FindUsersByFullName(string firsName, string lastName, string middleName,
        CancellationToken cancellationToken = default);
}

internal class PatientRepository : RepositoryBase<Patient>, IPatientRepository
{
    private readonly BlobUrlHelpers _blobUrlHelpers;
    private readonly UserManager<Account> _userManager;

    public PatientRepository(HospitalDbContext dbContext, IOptions<BlobUrlHelpers> blobUrlHelpers,
        UserManager<Account> userManager) : base(dbContext)
    {
        _blobUrlHelpers = blobUrlHelpers.Value;
        _userManager = userManager;
    }

    public async Task<long> GetPatientIdByAccountId(string accountId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Patients.Where(x => x.AccountId == accountId).Select(x => x.Id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<PatientAllDTO[]> GetAllPatientsAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Patients.Select(x => new PatientAllDTO
        {
            Id = x.Id,
            FirstName = x.Account.FirstName,
            LastName = x.Account.LastName,
            MiddleName = x.Account.MiddleName,
            PhoneNumber = x.Account.PhoneNumber
        }).ToArrayAsync(cancellationToken);
    }

    public async Task<PatientOneDTO?> GetPatientByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var currentTime = DateTime.Now.TimeOfDay;
        var appointments = await DbContext.Patients.Where(x => x.Id == id).Select(x =>x.Appointments
            .Where(y => y.PatientId == id && y.IsApproved && y.DateTime.Date <= DateTime.Now.Date)
            .GroupBy(y => y.SpecializationId)
            .Select(specGroup => new ResultForPatient
            {
                Title = specGroup.First().Specialization.Title,
                Children = specGroup
                    .GroupBy(a => a.ServiceId)
                    .Select(serviceGroup => new ServiceForPatientResultDto
                    {
                        Title = serviceGroup.First().Service.Title,
                        Children = serviceGroup.Where(appf=>appf.IsApproved && appf.Result.AppointmentId == appf.Id)
                            .Select(app => new AppointmentForPatientDto
                            {
                                Date = app.DateTime.ToShortDateString(),
                                DoctorFullName =
                                    $"{app.Doctor.Account.FirstName} {app.Doctor.Account.LastName} {app.Doctor.Account.MiddleName}",
                                Children = new List<ResultDto>
                                {
                                    new ResultDto
                                    {
                                        AbsoluteUrl = _blobUrlHelpers.AbsolutUrl + app.Result.Document.Path,
                                        FileTitle = app.Result.Document.FileName
                                    }
                                }
                            }).ToList()
                    }).ToList()
            }).ToList()).SingleOrDefaultAsync();
        return await DbContext.Patients.Where(x => x.Id == id).Select(x =>
            new PatientOneDTO
            {
                AccountId = x.AccountId,
                FirstName = x.Account.FirstName,
                LastName = x.Account.LastName,
                MiddleName = x.Account.MiddleName,
                PhoneNumber = x.Account.PhoneNumber,
                BirthDay = x.Account.Birthday,
                DocumentAbsolutUrl = _blobUrlHelpers.AbsolutUrl + x.Account.Photo.Path,
                Results = appointments
            }
        ).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<UsersDTO[]> FindUsersByFullName(string firsName, string lastName, string middleName,
        CancellationToken cancellationToken = default)
    {
        var lowerFirstNameTerm = firsName.ToLower();
        var lowerLastNameTerm = lastName.ToLower();
        var lowerMiddleNameTerm = middleName.ToLower();
        var matchExpressionFirstName = $"%{lowerFirstNameTerm}%";
        var matchExpressionLastName = $"%{lowerLastNameTerm}%";
        var matchExpressionMiddleName = $"%{lowerMiddleNameTerm}%";
        var users = await DbContext.Users.Where(x =>
            EF.Functions.Like(x.FirstName.ToLower(), matchExpressionFirstName) ||
            EF.Functions.Like(x.LastName.ToLower(), matchExpressionLastName) ||
            EF.Functions.Like(x.MiddleName.ToLower(), matchExpressionMiddleName)
        ).Where(x =>
            DbContext.UserRoles.Where(a => a.UserId == x.Id)
                .All(s => s.RoleId == DbContext.Roles.First(d => d.Name == UserRoles.User).Id) &&
            DbContext.UserRoles.Where(c => c.UserId == x.Id).Count() == 1).Select(x => new UsersDTO
        {
            AccountId = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            MiddleName = x.MiddleName,
            PhoneNumber = x.PhoneNumber
        }).ToArrayAsync(cancellationToken);
        return users;
    }
}