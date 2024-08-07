﻿using Appointment.API.Application.Contracts.Outgoing;
using Authorization.Data.Repository.Abstraction;
using Authorization.Data.Shared.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Linq;
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

    Task<BusyTimeSlotsDto[]>
        GetBusyTimeSlots(long docId, DateTime date,
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
            .Where(x => x.DateTime.Date == date.Date && x.DoctorId == doctorId && x.IsApproved)
            .Select(x => new AppointmentScheduleForDoctorDTO
            {
                AppointmentId = x.Id,
                ApprovedStatus = x.IsApproved,
                DateTime = x.DateTime,
                PatientFullName = string.Join(" ", x.Patient.Account.LastName, x.Patient.Account.FirstName,
                    x.Patient.Account.MiddleName),
                ServiceName = x.Service.Title,
                SpecializationName = x.Specialization.Title,
                ResultId = x.Result.Id,
                PatientId = x.PatientId,

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
                IsApprove = x.IsApproved,
                PatientId = x.PatientId
            }).ToArrayAsync(cancellationToken);
    }

    public async Task<AppointmentHistoryDTO[]>
        GetAppointmentsByPatientIdAsync(long patientId,
            CancellationToken cancellationToken = default)
    {
        return await DbContext.Appointments.AsNoTracking().Where(x => x.PatientId == patientId && DateTime.Now.Date <= x.DateTime.Date)
            .Select(x => new AppointmentHistoryDTO
            {
                DateTime = x.DateTime,
                DoctorFullName = string.Join(" ", x.Doctor.Account.LastName, x.Doctor.Account.FirstName,
                    x.Doctor.Account.MiddleName),
                ServiceName = x.Service.Title,
                IsApprove = x.IsApproved,
                SpecializationId = x.Specialization.Id,
                AppointmentId = x.Id,
            }).ToArrayAsync(cancellationToken);
    }
    public async Task<BusyTimeSlotsDto[]>GetBusyTimeSlots(long docId,DateTime date,CancellationToken cancellationToken = default)
    {
        var doc = await DbContext.Doctors.Where(x => x.Id == docId).Include(x => x.Status).FirstOrDefaultAsync(cancellationToken);
        if (doc != null && doc.Status.Title == "At work")
        {
            return await DbContext.Appointments.AsNoTracking().Where(x => x.DoctorId == docId && x.DateTime.Date == date.Date)
                .Select(x => new BusyTimeSlotsDto
                {
                    DatesTime = x.DateTime,
                    Duration = x.Service.ServiceCategory.TimeSlotSize
                }).ToArrayAsync(cancellationToken);

        }
        return Array.Empty<BusyTimeSlotsDto>();
    }
}