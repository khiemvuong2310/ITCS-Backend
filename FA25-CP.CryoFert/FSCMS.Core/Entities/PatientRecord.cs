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
        public int PatientID { get; set; }
        public int EncounterID { get; set; }
        public int ServiceID { get; set; }
        public int PaymentID { get; set; }
        public string Notes { get; set; }
    }
}
