using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    public class TreatmentTimeline : BaseEntity
    {
        public int PatientID { get; set; }
        public string Type { get; set; } // IUI/IVF/Cryo
        public string Step { get; set; }
        public string Status { get; set; }
        public int UpdatedBy { get; set; }
    }
}
