using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho báo cáo
    /// Lưu trữ các báo cáo thống kê về doanh thu, số lượng bệnh nhân, tỷ lệ thành công
    /// </summary>
    public class Report : BaseEntity
    {
        public string Type { get; set; } // Revenue, PatientCount, SuccessRate, SampleQuality
        public string Data { get; set; }
    }
}
