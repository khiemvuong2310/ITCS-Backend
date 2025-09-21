using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho bác sĩ trong hệ thống
    /// Chứa thông tin chuyên môn, chứng chỉ và lịch làm việc của bác sĩ
    /// </summary>
    public class Doctor : BaseEntity
    {
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public string Certificates { get; set; }
        public string Schedule { get; set; }
        public string Contact { get; set; }

        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
