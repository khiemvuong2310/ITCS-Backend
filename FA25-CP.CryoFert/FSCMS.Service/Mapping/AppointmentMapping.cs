using System;
using System.Linq;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.Mapping
{
    /// <summary>
    /// Helper/extensions for mapping Appointment entities to response models.
    /// Moved from AppointmentService to keep the service code clean.
    /// </summary>
    public static class AppointmentMapping
    {
        /// <summary>
        /// Map Appointment entity to AppointmentResponse.
        /// </summary>
        public static AppointmentResponse ToAppointmentResponse(this Appointment appointment)
        {
            if (appointment == null) throw new ArgumentNullException(nameof(appointment));

            var response = new AppointmentResponse
            {
                Id = appointment.Id,
                TreatmentCycleId = appointment.TreatmentCycleId,
                SlotId = appointment.SlotId,
                Type = appointment.Type,
                TypeName = appointment.Type.ToString(),
                Status = appointment.Status,
                StatusName = appointment.Status.ToString(),
                AppointmentDate = appointment.AppointmentDate,
                Reason = appointment.Reason,
                Instructions = appointment.Instructions,
                Notes = appointment.Notes,
                CheckInTime = appointment.CheckInTime,
                CheckOutTime = appointment.CheckOutTime,
                IsReminderSent = appointment.IsReminderSent,
                CreatedAt = appointment.CreatedAt,
                UpdatedAt = appointment.UpdatedAt
            };

            // Map TreatmentCycle
            if (appointment.TreatmentCycle != null)
            {
                response.TreatmentCycle = new AppointmentTreatmentCycleInfo
                {
                    Id = appointment.TreatmentCycle.Id,
                    CycleName = appointment.TreatmentCycle.CycleName,
                    CycleNumber = appointment.TreatmentCycle.CycleNumber,
                    StartDate = appointment.TreatmentCycle.StartDate,
                    EndDate = appointment.TreatmentCycle.EndDate,
                    Status = appointment.TreatmentCycle.Status.ToString()
                };

                // Map Treatment
                if (appointment.TreatmentCycle.Treatment != null)
                {
                    response.TreatmentCycle.Treatment = new AppointmentTreatmentInfo
                    {
                        Id = appointment.TreatmentCycle.Treatment.Id,
                        TreatmentType = appointment.TreatmentCycle.Treatment.TreatmentType.ToString()
                    };

                    // Map Patient from Treatment
                    if (appointment.TreatmentCycle.Treatment.Patient != null &&
                        appointment.TreatmentCycle.Treatment.Patient.Account != null)
                    {
                        var account = appointment.TreatmentCycle.Treatment.Patient.Account;
                        response.TreatmentCycle.Treatment.Patient = new AppointmentPatientInfo
                        {
                            Id = appointment.TreatmentCycle.Treatment.Patient.Id,
                            PatientCode = appointment.TreatmentCycle.Treatment.Patient.PatientCode,
                            FullName = $"{account.FirstName} {account.LastName}".Trim(),
                            Phone = account.Phone,
                            Email = account.Email
                        };
                    }
                }
            }

            // Map Patient (prefer direct relation; fallback to patient from treatment)
            if (appointment.Patient != null && appointment.Patient.Account != null)
            {
                var p = appointment.Patient;
                var pa = p.Account;
                response.Patient = new AppointmentPatientInfo
                {
                    Id = p.Id,
                    PatientCode = p.PatientCode,
                    FullName = $"{pa.FirstName} {pa.LastName}".Trim(),
                    Phone = pa.Phone,
                    Email = pa.Email
                };
            }
            else if (appointment.TreatmentCycle?.Treatment?.Patient != null &&
                     appointment.TreatmentCycle.Treatment.Patient.Account != null)
            {
                var p = appointment.TreatmentCycle.Treatment.Patient;
                var pa = p.Account;
                response.Patient = new AppointmentPatientInfo
                {
                    Id = p.Id,
                    PatientCode = p.PatientCode,
                    FullName = $"{pa.FirstName} {pa.LastName}".Trim(),
                    Phone = pa.Phone,
                    Email = pa.Email
                };
            }

            // Map Slot
            if (appointment.Slot != null)
            {
                response.Slot = new AppointmentSlotInfo
                {
                    Id = appointment.Slot.Id,
                    StartTime = appointment.Slot.StartTime,
                    EndTime = appointment.Slot.EndTime,
                    IsBooked = appointment.Slot.Appointments.Any(a =>
                        !a.IsDeleted &&
                        a.Status != AppointmentStatus.Cancelled &&
                        a.Status != AppointmentStatus.Completed)
                };

                // Map Schedule: pick schedule for the appointment date if available, otherwise first by date
                if (appointment.Slot.DoctorSchedules != null && appointment.Slot.DoctorSchedules.Any())
                {
                    var apptDateOnly = appointment.AppointmentDate;
                    var matchedSchedule = appointment.Slot.DoctorSchedules
                        .FirstOrDefault(ds => ds.WorkDate == apptDateOnly);
                    var schedule = matchedSchedule ?? appointment.Slot.DoctorSchedules
                        .OrderBy(ds => ds.WorkDate)
                        .FirstOrDefault();

                    if (schedule != null)
                    {
                        response.Slot.Schedule = new AppointmentScheduleInfo
                        {
                            Id = schedule.Id,
                            WorkDate = schedule.WorkDate.ToDateTime(TimeOnly.MinValue),
                            Location = schedule.Location
                        };

                        // Map Doctor from Schedule
                        if (schedule.Doctor != null && schedule.Doctor.Account != null)
                        {
                            var account = schedule.Doctor.Account;
                            response.Slot.Schedule.Doctor = new AppointmentDoctorBasicInfo
                            {
                                Id = schedule.Doctor.Id,
                                BadgeId = schedule.Doctor.BadgeId,
                                Specialty = schedule.Doctor.Specialty,
                                FullName = $"{account.FirstName} {account.LastName}".Trim()
                            };
                        }
                    }
                }
            }

            // Map Doctors
            if (appointment.AppointmentDoctors != null && appointment.AppointmentDoctors.Any())
            {
                response.Doctors = appointment.AppointmentDoctors
                    .Where(ad => !ad.IsDeleted && ad.Doctor != null && ad.Doctor.Account != null)
                    .Select(ad =>
                    {
                        var account = ad.Doctor!.Account!;
                        return new AppointmentDoctorInfo
                        {
                            Id = ad.Id,
                            DoctorId = ad.DoctorId,
                            BadgeId = ad.Doctor.BadgeId,
                            Specialty = ad.Doctor.Specialty,
                            FullName = $"{account.FirstName} {account.LastName}".Trim(),
                            Role = ad.Role,
                            Notes = ad.Notes
                        };
                    })
                    .ToList();
            }

            response.DoctorCount = response.Doctors.Count;

            return response;
        }

        /// <summary>
        /// Map Appointment entity to AppointmentDetailResponse.
        /// </summary>
        public static AppointmentDetailResponse ToAppointmentDetailResponse(this Appointment appointment)
        {
            if (appointment == null) throw new ArgumentNullException(nameof(appointment));

            var baseResponse = appointment.ToAppointmentResponse();

            var detailResponse = new AppointmentDetailResponse
            {
                Id = baseResponse.Id,
                TreatmentCycleId = baseResponse.TreatmentCycleId,
                SlotId = baseResponse.SlotId,
                Type = baseResponse.Type,
                TypeName = baseResponse.TypeName,
                Status = baseResponse.Status,
                StatusName = baseResponse.StatusName,
                AppointmentDate = baseResponse.AppointmentDate,
                Reason = baseResponse.Reason,
                Instructions = baseResponse.Instructions,
                Notes = baseResponse.Notes,
                CheckInTime = baseResponse.CheckInTime,
                CheckOutTime = baseResponse.CheckOutTime,
                IsReminderSent = baseResponse.IsReminderSent,
                CreatedAt = baseResponse.CreatedAt,
                UpdatedAt = baseResponse.UpdatedAt,
                Patient = baseResponse.Patient,
                TreatmentCycle = baseResponse.TreatmentCycle,
                Slot = baseResponse.Slot,
                Doctors = baseResponse.Doctors,
                DoctorCount = baseResponse.DoctorCount
            };

            // Map MedicalRecords
            if (appointment.MedicalRecords != null && appointment.MedicalRecords.Any())
            {
                detailResponse.MedicalRecords = appointment.MedicalRecords
                    .Where(mr => !mr.IsDeleted)
                    .Select(mr => new AppointmentMedicalRecordInfo
                    {
                        Id = mr.Id,
                        RecordDate = mr.CreatedAt,
                        ChiefComplaint = mr.ChiefComplaint,
                        Diagnosis = mr.Diagnosis
                    })
                    .OrderByDescending(mr => mr.RecordDate)
                    .ToList();
            }

            return detailResponse;
        }
    }
}


