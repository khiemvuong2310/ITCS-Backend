using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    public class Report : BaseEntity
    {
        public string Type { get; set; } // Revenue, PatientCount, SuccessRate, SampleQuality
        public string Data { get; set; }
    }
}
