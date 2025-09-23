using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho đơn thuốc
    /// Ghi lại thông tin đơn thuốc được bác sĩ kê cho bệnh nhân
    /// </summary>
    public class Prescription : BaseEntity
    {
        public int EncounterId { get; set; }
        public int PatientId { get; set; }
        public string Medicine { get; set; }
        public string Dosage { get; set; }
        public string Route { get; set; }
        public string Duration { get; set; }
        public string Notes { get; set; }

        public virtual Encounter? Encounter { get; set; }
        public virtual Patient? Patient { get; set; }
    }
}
