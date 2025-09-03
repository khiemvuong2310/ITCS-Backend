using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    public class IVFProcedure : BaseEntity
    {
        public int PatientID { get; set; }
        public string Protocol { get; set; }
        public string StimulationDrugs { get; set; }
        public string Monitoring { get; set; }
        public string OPUDetails { get; set; }
        public string SpermPrep { get; set; }
        public string Fertilization { get; set; }
        public string EmbryoCulture { get; set; }
        public string EmbryoTransfer { get; set; }
        public string LutealSupport { get;         set; }
        public string PregnancyResult { get; set; }
        public string CycleStatus { get; set; }
    }
}
