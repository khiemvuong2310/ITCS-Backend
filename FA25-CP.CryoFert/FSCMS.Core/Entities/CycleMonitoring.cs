using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho việc theo dõi chu trình điều trị
    /// Ghi lại kết quả siêu âm, xét nghiệm máu và điều chỉnh thuốc trong quá trình điều trị
    /// </summary>
    public class CycleMonitoring : BaseEntity
    {
        public int TreatmentCycleId { get; set; }
        public int CycleDay { get; set; }
        public DateTime MonitoringDate { get; set; }
        
        // Ultrasound Findings
        public decimal? EndometrialThickness { get; set; } // mm
        public int? FollicleCount { get; set; }
        public string? FollicleSizes { get; set; } // JSON or comma-separated sizes
        public string? UltrasoundNotes { get; set; }
        
        // Blood Work
        public decimal? E2Level { get; set; } // Estradiol
        public decimal? LHLevel { get; set; }
        public decimal? FSHLevel { get; set; }
        public decimal? ProgesteroneLevel { get; set; }
        
        // Medication Adjustments
        public string? MedicationChanges { get; set; }
        public string? NextSteps { get; set; }
        
        public int MonitoredByUserId { get; set; }
        public string? Notes { get; set; }

        // Navigation Properties
        public virtual TreatmentCycle? TreatmentCycle { get; set; }
        public virtual User? MonitoredByUser { get; set; }
    }
}
