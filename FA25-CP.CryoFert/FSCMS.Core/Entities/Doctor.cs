using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
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
