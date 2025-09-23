using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho hồ sơ bệnh nhân
    /// Liên kết thông tin bệnh nhân với các cuộc khám, dịch vụ và thanh toán
    /// </summary>
    public class PatientRecord : BaseEntity
    {
        public int PatientId { get; set; }
        public int EncounterId { get; set; }
        public int ServiceId { get; set; }
        public int PaymentId { get; set; }
        public string Notes { get; set; }

        public virtual Patient? Patient { get; set; }
        public virtual Encounter? Encounter { get; set; }
        public virtual Service? Service { get; set; }
        public virtual Payment? Payment { get; set; }
    }
}
