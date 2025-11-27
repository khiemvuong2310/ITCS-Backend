using System;
using System.ComponentModel.DataAnnotations;
using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;

namespace FSCMS.Service.RequestModel
{
    public enum TemplateType

    {
        MedicalRecord = 0,
        //TreatmentCycle = 1,
        Agreement = 2,
        CryoStorageContract = 3,
    }

    public class GenerateFilledPdfRequest
    {
        [Required(ErrorMessage = "TemplateType is required.")]
        public TemplateType TemplateType { get; set; }
        [Required(ErrorMessage = "RelatedEntityId is required.")]
        public Guid RelatedEntityId { get; set; }
    }

    public class TemplateDataModel
    {
        public Patient? Patient { get; set; }
        public MedicalRecord? MedicalRecord { get; set; }
        public TreatmentCycle? TreatmentCycle { get; set; }
        public Agreement? Agreement { get; set; }
        public Appointment? Appointment { get; set; }
        public Treatment? Treatment { get; set; }
        public Doctor? Doctor { get; set; }
        public CryoStorageContract? CryoStorageContract { get; set; }
        public CryoPackage? CryoPackage { get; set; }

        public DateTime GeneratedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
