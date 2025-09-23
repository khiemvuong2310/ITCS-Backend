using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Entity đại diện cho dịch vụ y tế
    /// Định nghĩa các dịch vụ mà phòng khám cung cấp với giá cả và đơn vị tính
    /// </summary>
    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public bool ActiveFlag { get; set; }
        public int? ServiceProviderId { get; set; }

        public virtual ServiceProvider? ServiceProvider { get; set; }
    }
}
