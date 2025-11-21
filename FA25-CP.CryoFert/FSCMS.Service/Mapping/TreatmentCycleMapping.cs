using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Mapping
{
    public static class TreatmentCycleMapping
    {
        public static TreatmentCycleResponseModel ToResponseModel(this TreatmentCycle entity)
        {
            return new TreatmentCycleResponseModel
            {
                Id = entity.Id,
                TreatmentId = entity.TreatmentId,
                CycleName = entity.CycleName,
                CycleNumber = entity.CycleNumber,
                OrderIndex = entity.OrderIndex,
                StepType = entity.StepType,
                ExpectedDurationDays = entity.ExpectedDurationDays,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Status = entity.Status,
                Protocol = entity.Protocol,
                Notes = entity.Notes,
                Cost = entity.Cost,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static TreatmentCycleDetailResponseModel ToDetailResponseModel(this TreatmentCycle entity)
        {
            return new TreatmentCycleDetailResponseModel
            {
                Id = entity.Id,
                TreatmentId = entity.TreatmentId,
                CycleName = entity.CycleName,
                CycleNumber = entity.CycleNumber,
                OrderIndex = entity.OrderIndex,
                StepType = entity.StepType,
                ExpectedDurationDays = entity.ExpectedDurationDays,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Status = entity.Status,
                Protocol = entity.Protocol,
                Notes = entity.Notes,
                Cost = entity.Cost,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                PatientId = entity.Treatment?.PatientId ?? Guid.Empty,
                DoctorId = entity.Treatment?.DoctorId ?? Guid.Empty,
                TreatmentName = entity.Treatment?.TreatmentName
            };
        }

        public static TreatmentCycle ToEntity(this CreateTreatmentCycleRequest request)
        {
            return new TreatmentCycle(
                Guid.NewGuid(),
                request.TreatmentId,
                request.CycleName,
                request.CycleNumber,
                request.StartDate,
                TreatmentStepType.IUI_PreCyclePreparation,
                request.CycleNumber,
                0)
            {
                EndDate = request.EndDate,
                Protocol = request.Protocol,
                Notes = request.Notes,
                Cost = request.Cost
            };
        }

        public static void UpdateEntity(this TreatmentCycle entity, UpdateTreatmentCycleRequest request)
        {
            if (request.CycleName != null) entity.CycleName = request.CycleName;
            if (request.CycleNumber.HasValue) entity.CycleNumber = request.CycleNumber.Value;
            if (request.StartDate.HasValue) entity.StartDate = request.StartDate.Value;
            if (request.EndDate.HasValue) entity.EndDate = request.EndDate.Value;
            if (request.Protocol != null) entity.Protocol = request.Protocol;
            if (request.Notes != null) entity.Notes = request.Notes;
            if (request.Cost.HasValue) entity.Cost = request.Cost.Value;
            if (request.Status.HasValue) entity.Status = request.Status.Value;
        }
    }
}


