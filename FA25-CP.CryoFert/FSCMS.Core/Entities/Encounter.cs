using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho cuộc khám bệnh
    /// Ghi lại thông tin chi tiết về lần khám của bệnh nhân với bác sĩ
    /// </summary>
    public class Encounter : BaseEntity
    {
        public int PatientID { get; set; }
        public DateTime VisitDate { get; set; }
        public int ProviderID { get; set; }
        public string ChiefComplaint { get; set; }
        public string History { get; set; }
        public string Vitals { get; set; }
        public string Diagnosis { get; set; }
        public string Orders { get; set; }
        public string Prescriptions { get; set; }
        public string Attachments { get; set; }
        public string ReferralTo { get; set; }
        public string Status { get; set; }

        public virtual User? Patient { get; set; }
        public virtual User? Provider { get; set; }
    }
}
