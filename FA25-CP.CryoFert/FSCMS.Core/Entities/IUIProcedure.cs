using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    public class IUIProcedure : BaseEntity
    {
        public int PatientID { get; set; }
        public string TriggerMeds { get; set; }
        public string UltrasoundResult { get; set; }
        public string SpermPrep { get; set; }
        public string InseminationDetails { get; set; }
        public string LutealSupport { get; set; }
        public string PregnancyTestResult { get; set; }
        public string Status { get; set; }
    }
}
