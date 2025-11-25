using FSCMS.Core.Enum;

namespace FSCMS.Service.ReponseModel
{
    public class TreatmentResponseModel
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public string TreatmentName { get; set; } = string.Empty;
        public TreatmentType TreatmentType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TreatmentStatus Status { get; set; }
        public string? Diagnosis { get; set; }
        public string? Goals { get; set; }
        public string? Notes { get; set; }
        public decimal? EstimatedCost { get; set; }
        public decimal? ActualCost { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<AgreementResponse>? Agreements { get; set; }
    }

    public class TreatmentDetailResponseModel : TreatmentResponseModel
    {
        public TreatmentIVFResponseModel? IVF { get; set; }
        public TreatmentIUIResponseModel? IUI { get; set; }
        // Agreements already included in base class
    }

    public class TreatmentWithCyclesResponseModel : TreatmentResponseModel
    {
        public List<TreatmentCycleResponseModel> AutoCreatedCycles { get; set; } = new();
    }

    public class TreatmentIVFResponseModel
    {
        public Guid Id { get; set; }
        public Guid TreatmentId { get; set; }
        public string Protocol { get; set; } = string.Empty;
        public DateTime? StimulationStartDate { get; set; }
        public DateTime? OocyteRetrievalDate { get; set; }
        public DateTime? FertilizationDate { get; set; }
        public DateTime? TransferDate { get; set; }
        public int? OocytesRetrieved { get; set; }
        public int? OocytesMature { get; set; }
        public int? OocytesFertilized { get; set; }
        public int? EmbryosCultured { get; set; }
        public int? EmbryosTransferred { get; set; }
        public int? EmbryosCryopreserved { get; set; }
        public int? EmbryosFrozen { get; set; }
        public string? Notes { get; set; }
        public string? Outcome { get; set; }
        public bool? UsedICSI { get; set; }
        public string? Complications { get; set; }
        public IVFCycleStatus Status { get; set; }
        public int CurrentStep { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<AgreementResponse>? Agreements { get; set; }
    }

    public class TreatmentIUIResponseModel
    {
        public Guid Id { get; set; }
        public Guid TreatmentId { get; set; }
        public string Protocol { get; set; } = string.Empty;
        public string? Medications { get; set; }
        public string? Monitoring { get; set; }
        public DateTime? OvulationTriggerDate { get; set; }
        public DateTime? InseminationDate { get; set; }
        public int? MotileSpermCount { get; set; }
        public int? NumberOfAttempts { get; set; }
        public string? Outcome { get; set; }
        public string? Notes { get; set; }
        public IUICycleStatus Status { get; set; }
        public int CurrentStep { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<AgreementResponse>? Agreements { get; set; }
    }
}


