using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    public class Prescription : BaseEntity
    {
        public int EncounterID { get; set; }
        public int PatientID { get; set; }
        public string Medicine { get; set; }
        public string Dosage { get; set; }
        public string Route { get; set; }
        public string Duration { get; set; }
        public string Notes { get; set; }
    }
}
