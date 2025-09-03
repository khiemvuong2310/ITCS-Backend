using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    public class SpecimenQuality : BaseEntity
    {
        public int SpecimenID { get; set; }
        public string Parameters { get; set; } // Density, Morphology, PR%, BlastocystGrade, etc.
        public string Result { get; set; }
        public string Status { get; set; }
    }
}
