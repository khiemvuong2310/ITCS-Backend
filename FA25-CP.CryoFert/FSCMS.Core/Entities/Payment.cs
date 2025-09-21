using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho thanh toán
    /// Ghi lại thông tin thanh toán của bệnh nhân cho các dịch vụ
    /// </summary>
    public class Payment : BaseEntity
    {
        public int PatientID { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }

        public virtual User? Patient { get; set; }
    }
}
