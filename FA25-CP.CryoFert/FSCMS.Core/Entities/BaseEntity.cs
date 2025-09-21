using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Lớp cơ sở cho tất cả các entity trong hệ thống
    /// Chứa các thuộc tính chung như Id, thời gian tạo, cập nhật và trạng thái xóa
    /// </summary>
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;   
        public bool IsDelete { get; set; }
    }
}
