using Appointment.API.Application.Contracts.Outgoing;
using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Microsoft.EntityFrameworkCore;
using AppointmentEntity = Authorization.Data_Domain.Models.Appointment;

namespace Authorization.Data.Repository;

public interface IAppointmentRepository : IRepositoryBase<AppointmentEntity>
{
    public Task<AppointmentScheduleForDoctorDTO[]> GetAppointmentScheduleForDoctorByCurrentDayAsync(DateTime date,
        long doctorId,
        CancellationToken cancellationToken = default);

    public Task<AppointmentScheduleForReceptionistDTO[]>
        GetAppointmentScheduleForReceptionistByCurrentDayAsync(DateTime date, long officeId,
            CancellationToken cancellationToken = default);

    public Task<AppointmentHistoryDTO[]>
        GetAppointmentsByPatientIdAsync(long patientId,
            CancellationToken cancellationToken = default);
}

internal class AppointmentRepository : RepositoryBase<AppointmentEntity>, IAppointmentRepository
{
    public AppointmentRepository(HospitalDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<AppointmentScheduleForDoctorDTO[]> GetAppointmentScheduleForDoctorByCurrentDayAsync(DateTime date,
        long doctorId,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Appointments.AsNoTracking()
            .Where(x => x.DateTime.Date == date.Date && x.DoctorId == doctorId)
            .Select(x => new AppointmentScheduleForDoctorDTO
            {
                AppointmentId = x.Id,
                ApprovedStatus = x.IsApproved,
                DateTime = x.DateTime,
                PatientFullName = string.Join(" ", x.Patient.Account.LastName, x.Patient.Account.FirstName,
                    x.Patient.Account.MiddleName),
                ServiceName = x.Service.Title

            }).ToArrayAsync(cancellationToken);
    }

    public async Task<AppointmentScheduleForReceptionistDTO[]> GetAppointmentScheduleForReceptionistByCurrentDayAsync(
        DateTime date, long officeId,
        CancellationToken cancellationToken = default)
    {
     
        return await DbContext.Appointments.AsNoTracking()
            .Where(x => x.DateTime.Date == date.Date && x.OfficeId == officeId)
            .Select(x => new AppointmentScheduleForReceptionistDTO
            {
                AppointmentId = x.Id,
                DateTime = x.DateTime,
                PatientFullName = string.Join(" ", x.Patient.Account.LastName, x.Patient.Account.FirstName,
                    x.Patient.Account.MiddleName),
                DoctorFullName = string.Join(" ", x.Doctor.Account.LastName, x.Doctor.Account.FirstName,
                    x.Doctor.Account.MiddleName),
                ServiceName = x.Service.Title,
                PatientPhoneNumber = x.Patient.Account.PhoneNumber,
                SpecializationName = x.Specialization.Title,
                IsApprove = x.IsApproved
            }).ToArrayAsync(cancellationToken);
    }

    public async Task<AppointmentHistoryDTO[]>
        GetAppointmentsByPatientIdAsync(long patientId,
            CancellationToken cancellationToken = default)
    {
        return await DbContext.Appointments.AsNoTracking().Where(x => x.PatientId == patientId)
            .Select(x => new AppointmentHistoryDTO
            {
                DateTime = x.DateTime,
                DoctorFullName = string.Join(" ", x.Doctor.Account.LastName, x.Doctor.Account.FirstName,
                    x.Doctor.Account.MiddleName),
                ServiceName = x.Service.Title
            }).ToArrayAsync(cancellationToken);
    }
}