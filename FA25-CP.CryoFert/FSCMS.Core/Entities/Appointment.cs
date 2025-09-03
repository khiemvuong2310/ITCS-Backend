using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    public class Appointment : BaseEntity
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public virtual User? Patient { get; set; }
        public virtual Doctor? Doctor { get; set; }
    }
}
