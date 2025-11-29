using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Mapping
{
    public static class TreatmentMapping
    {
        /// <summary>
        /// Maps Agreement entity to AgreementResponse
        /// </summary>
        public static AgreementResponse ToAgreementResponse(this Agreement entity)
        {
            if (entity == null) return null!;
            
            return new AgreementResponse
            {
                Id = entity.Id,
                AgreementCode = entity.AgreementCode,
                TreatmentId = entity.TreatmentId,
                TreatmentName = entity.Treatment?.TreatmentName,
                PatientId = entity.PatientId,
                PatientName = entity.Patient?.Account != null 
                    ? $"{entity.Patient.Account.FirstName} {entity.Patient.Account.LastName}".Trim() 
                    : null,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                TotalAmount = entity.TotalAmount,
                Status = entity.Status,
                SignedByPatient = entity.SignedByPatient,
                SignedByDoctor = entity.SignedByDoctor,
                FileUrl = entity.FileUrl,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
        public static TreatmentResponseModel ToResponseModel(this Treatment entity)
        {
            return new TreatmentResponseModel
            {
                Id = entity.Id,
                PatientId = entity.PatientId,
                DoctorId = entity.DoctorId,
                TreatmentName = entity.TreatmentName,
                TreatmentType = entity.TreatmentType,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Status = entity.Status,
                Diagnosis = entity.Diagnosis,
                Goals = entity.Goals,
                Notes = entity.Notes,
                EstimatedCost = entity.EstimatedCost,
                ActualCost = entity.ActualCost,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static TreatmentDetailResponseModel ToDetailResponseModel(this Treatment entity)
        {
            return new TreatmentDetailResponseModel
            {
                Id = entity.Id,
                PatientId = entity.PatientId,
                DoctorId = entity.DoctorId,
                TreatmentName = entity.TreatmentName,
                TreatmentType = entity.TreatmentType,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Status = entity.Status,
                Diagnosis = entity.Diagnosis,
                Goals = entity.Goals,
                Notes = entity.Notes,
                EstimatedCost = entity.EstimatedCost,
                ActualCost = entity.ActualCost,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                // IVF and IUI will be set explicitly in service based on TreatmentType
                IVF = null,
                IUI = null
            };
        }

        public static Treatment ToEntity(this TreatmentCreateUpdateRequest request)
        {
            return new Treatment(
                Guid.NewGuid(),
                request.PatientId,
                request.DoctorId,
                request.TreatmentName,
                request.TreatmentType,
                request.StartDate
            )
            {
                EndDate = request.EndDate,
                Diagnosis = request.Diagnosis,
                Goals = request.Goals,
                Notes = request.Notes,
                EstimatedCost = request.EstimatedCost,
                ActualCost = request.ActualCost,
                Status = request.Status ?? Core.Enum.TreatmentStatus.Planned
            };
        }

        public static void UpdateEntity(this Treatment entity, TreatmentCreateUpdateRequest request)
        {
            entity.PatientId = request.PatientId;
            entity.DoctorId = request.DoctorId;
            entity.TreatmentName = request.TreatmentName;
            entity.TreatmentType = request.TreatmentType;
            entity.StartDate = request.StartDate;
            entity.EndDate = request.EndDate;
            entity.Diagnosis = request.Diagnosis;
            entity.Goals = request.Goals;
            entity.Notes = request.Notes;
            entity.EstimatedCost = request.EstimatedCost;
            entity.ActualCost = request.ActualCost;
            if (request.Status.HasValue)
            {
                entity.Status = request.Status.Value;
            }
        }

        /// <summary>
        /// Partial update - only updates fields that are provided (not null)
        /// </summary>
        public static void UpdateEntity(this Treatment entity, TreatmentUpdateRequest request)
        {
            if (request.PatientId.HasValue)
            {
                entity.PatientId = request.PatientId.Value;
            }

            if (request.DoctorId.HasValue)
            {
                entity.DoctorId = request.DoctorId.Value;
            }

            if (!string.IsNullOrWhiteSpace(request.TreatmentName))
            {
                entity.TreatmentName = request.TreatmentName;
            }

            if (request.TreatmentType.HasValue)
            {
                entity.TreatmentType = request.TreatmentType.Value;
            }

            if (request.StartDate.HasValue)
            {
                entity.StartDate = request.StartDate.Value;
            }

            if (request.EndDate.HasValue)
            {
                entity.EndDate = request.EndDate.Value;
            }

            if (request.Status.HasValue)
            {
                entity.Status = request.Status.Value;
            }

            // For nullable string fields, allow setting to null explicitly
            if (request.Diagnosis != null)
            {
                entity.Diagnosis = request.Diagnosis;
            }

            if (request.Goals != null)
            {
                entity.Goals = request.Goals;
            }

            if (request.Notes != null)
            {
                entity.Notes = request.Notes;
            }

            if (request.EstimatedCost.HasValue)
            {
                entity.EstimatedCost = request.EstimatedCost.Value;
            }

            if (request.ActualCost.HasValue)
            {
                entity.ActualCost = request.ActualCost.Value;
            }
        }

        public static TreatmentIVFResponseModel ToResponseModel(this TreatmentIVF entity)
        {
            return new TreatmentIVFResponseModel
            {
                Id = entity.Id,
                Protocol = entity.Protocol,
                StimulationStartDate = entity.StimulationStartDate,
                OocyteRetrievalDate = entity.OocyteRetrievalDate,
                FertilizationDate = entity.FertilizationDate,
                TransferDate = entity.TransferDate,
                OocytesRetrieved = entity.OocytesRetrieved,
                OocytesMature = entity.OocytesMature,
                OocytesFertilized = entity.OocytesFertilized,
                EmbryosCultured = entity.EmbryosCultured,
                EmbryosTransferred = entity.EmbryosTransferred,
                EmbryosCryopreserved = entity.EmbryosCryopreserved,
                EmbryosFrozen = entity.EmbryosFrozen,
                Notes = entity.Notes,
                Outcome = entity.Outcome,
                UsedICSI = entity.UsedICSI,
                Complications = entity.Complications,
                Status = entity.Status,
                CurrentStep = entity.CurrentStep,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static TreatmentIVF ToEntity(this TreatmentIVFCreateUpdateRequest request)
        {
            return new TreatmentIVF(request.TreatmentId, request.Protocol)
            {
                StimulationStartDate = request.StimulationStartDate,
                OocyteRetrievalDate = request.OocyteRetrievalDate,
                FertilizationDate = request.FertilizationDate,
                TransferDate = request.TransferDate,
                OocytesRetrieved = request.OocytesRetrieved,
                OocytesMature = request.OocytesMature,
                OocytesFertilized = request.OocytesFertilized,
                EmbryosCultured = request.EmbryosCultured,
                EmbryosTransferred = request.EmbryosTransferred,
                EmbryosCryopreserved = request.EmbryosCryopreserved,
                EmbryosFrozen = request.EmbryosFrozen,
                Notes = request.Notes,
                Outcome = request.Outcome,
                UsedICSI = request.UsedICSI,
                Complications = request.Complications,
                Status = request.Status ?? Core.Enum.IVFCycleStatus.Planned,
                CurrentStep = request.CurrentStep ?? 0
            };
        }

        public static void UpdateEntity(this TreatmentIVF entity, TreatmentIVFCreateUpdateRequest request)
        {
            entity.Protocol = request.Protocol;
            entity.StimulationStartDate = request.StimulationStartDate;
            entity.OocyteRetrievalDate = request.OocyteRetrievalDate;
            entity.FertilizationDate = request.FertilizationDate;
            entity.TransferDate = request.TransferDate;
            entity.OocytesRetrieved = request.OocytesRetrieved;
            entity.OocytesMature = request.OocytesMature;
            entity.OocytesFertilized = request.OocytesFertilized;
            entity.EmbryosCultured = request.EmbryosCultured;
            entity.EmbryosTransferred = request.EmbryosTransferred;
            entity.EmbryosCryopreserved = request.EmbryosCryopreserved;
            entity.EmbryosFrozen = request.EmbryosFrozen;
            entity.Notes = request.Notes;
            entity.Outcome = request.Outcome;
            entity.UsedICSI = request.UsedICSI;
            entity.Complications = request.Complications;
            if (request.Status.HasValue)
            {
                entity.Status = request.Status.Value;
            }
            if (request.CurrentStep.HasValue)
            {
                entity.CurrentStep = request.CurrentStep.Value;
            }
        }

        public static void UpdateEntity(this TreatmentIVF entity, TreatmentIVFUpdateRequest request)
        {
            if (!string.IsNullOrEmpty(request.Protocol))
            {
                entity.Protocol = request.Protocol;
            }
            if (request.StimulationStartDate.HasValue)
            {
                entity.StimulationStartDate = request.StimulationStartDate;
            }
            if (request.OocyteRetrievalDate.HasValue)
            {
                entity.OocyteRetrievalDate = request.OocyteRetrievalDate;
            }
            if (request.FertilizationDate.HasValue)
            {
                entity.FertilizationDate = request.FertilizationDate;
            }
            if (request.TransferDate.HasValue)
            {
                entity.TransferDate = request.TransferDate;
            }
            if (request.OocytesRetrieved.HasValue)
            {
                entity.OocytesRetrieved = request.OocytesRetrieved;
            }
            if (request.OocytesMature.HasValue)
            {
                entity.OocytesMature = request.OocytesMature;
            }
            if (request.OocytesFertilized.HasValue)
            {
                entity.OocytesFertilized = request.OocytesFertilized;
            }
            if (request.EmbryosCultured.HasValue)
            {
                entity.EmbryosCultured = request.EmbryosCultured;
            }
            if (request.EmbryosTransferred.HasValue)
            {
                entity.EmbryosTransferred = request.EmbryosTransferred;
            }
            if (request.EmbryosCryopreserved.HasValue)
            {
                entity.EmbryosCryopreserved = request.EmbryosCryopreserved;
            }
            if (request.EmbryosFrozen.HasValue)
            {
                entity.EmbryosFrozen = request.EmbryosFrozen;
            }
            if (request.Notes != null)
            {
                entity.Notes = request.Notes;
            }
            if (request.Outcome != null)
            {
                entity.Outcome = request.Outcome;
            }
            if (request.UsedICSI.HasValue)
            {
                entity.UsedICSI = request.UsedICSI;
            }
            if (request.Complications != null)
            {
                entity.Complications = request.Complications;
            }
            if (request.Status.HasValue)
            {
                entity.Status = request.Status.Value;
            }
            if (request.CurrentStep.HasValue)
            {
                entity.CurrentStep = request.CurrentStep.Value;
            }
        }

        public static TreatmentIUIResponseModel ToResponseModel(this TreatmentIUI entity)
        {
            return new TreatmentIUIResponseModel
            {
                Id = entity.Id,
                Protocol = entity.Protocol,
                Medications = entity.Medications,
                Monitoring = entity.Monitoring,
                OvulationTriggerDate = entity.OvulationTriggerDate,
                InseminationDate = entity.InseminationDate,
                MotileSpermCount = entity.MotileSpermCount,
                NumberOfAttempts = entity.NumberOfAttempts,
                Outcome = entity.Outcome,
                Notes = entity.Notes,
                Status = entity.Status,
                CurrentStep = entity.CurrentStep,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static TreatmentIUI ToEntity(this TreatmentIUICreateUpdateRequest request)
        {
            return new TreatmentIUI(request.TreatmentId, request.Protocol)
            {
                Medications = request.Medications,
                Monitoring = request.Monitoring,
                OvulationTriggerDate = request.OvulationTriggerDate,
                InseminationDate = request.InseminationDate,
                MotileSpermCount = request.MotileSpermCount,
                NumberOfAttempts = request.NumberOfAttempts,
                Outcome = request.Outcome,
                Notes = request.Notes,
                Status = request.Status ?? Core.Enum.IUICycleStatus.Planned,
                CurrentStep = request.CurrentStep ?? 0
            };
        }

        public static void UpdateEntity(this TreatmentIUI entity, TreatmentIUICreateUpdateRequest request)
        {
            entity.Protocol = request.Protocol;
            entity.Medications = request.Medications;
            entity.Monitoring = request.Monitoring;
            entity.OvulationTriggerDate = request.OvulationTriggerDate;
            entity.InseminationDate = request.InseminationDate;
            entity.MotileSpermCount = request.MotileSpermCount;
            entity.NumberOfAttempts = request.NumberOfAttempts;
            entity.Outcome = request.Outcome;
            entity.Notes = request.Notes;
            if (request.Status.HasValue)
            {
                entity.Status = request.Status.Value;
            }
            if (request.CurrentStep.HasValue)
            {
                entity.CurrentStep = request.CurrentStep.Value;
            }
        }

        public static void UpdateEntity(this TreatmentIUI entity, TreatmentIUIUpdateRequest request)
        {
            if (!string.IsNullOrEmpty(request.Protocol))
            {
                entity.Protocol = request.Protocol;
            }
            if (request.Medications != null)
            {
                entity.Medications = request.Medications;
            }
            if (request.Monitoring != null)
            {
                entity.Monitoring = request.Monitoring;
            }
            if (request.OvulationTriggerDate.HasValue)
            {
                entity.OvulationTriggerDate = request.OvulationTriggerDate;
            }
            if (request.InseminationDate.HasValue)
            {
                entity.InseminationDate = request.InseminationDate;
            }
            if (request.MotileSpermCount.HasValue)
            {
                entity.MotileSpermCount = request.MotileSpermCount;
            }
            if (request.NumberOfAttempts.HasValue)
            {
                entity.NumberOfAttempts = request.NumberOfAttempts;
            }
            if (request.Outcome != null)
            {
                entity.Outcome = request.Outcome;
            }
            if (request.Notes != null)
            {
                entity.Notes = request.Notes;
            }
            if (request.Status.HasValue)
            {
                entity.Status = request.Status.Value;
            }
            if (request.CurrentStep.HasValue)
            {
                entity.CurrentStep = request.CurrentStep.Value;
            }
        }
    }
}


