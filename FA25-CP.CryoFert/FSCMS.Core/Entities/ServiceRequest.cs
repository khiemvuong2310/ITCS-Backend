using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    public class ServiceRequest : BaseEntity
    {
        public int PatientID { get; set; }
        public int ServiceID { get; set; }
        public int PackageID { get; set; }
        public int DoctorID { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }

        public virtual User? Patient { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual Service? Service { get; set; }
    }
}
