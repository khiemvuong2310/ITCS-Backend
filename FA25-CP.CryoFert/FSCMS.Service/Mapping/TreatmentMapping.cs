using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Mapping
{
    public static class TreatmentMapping
    {
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
                IVF = entity.TreatmentIVF?.ToResponseModel(),
                IUI = null // IUI relation not on Treatment entity; managed separately if needed
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
                Status = request.Status ?? Core.Enum.IVFCycleStatus.Planned
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
                Status = request.Status ?? Core.Enum.IUICycleStatus.Planned
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
        }
    }
}


