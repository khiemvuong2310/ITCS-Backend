using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    public class Specimen : BaseEntity
    {
        public string Type { get; set; } // Egg/Sperm/Embryo
        public string Method { get; set; }
        public string Quality { get; set; }
        public string TankCode { get; set; }
        public string Cane { get; set; }
        public string Position { get; set; }
        public DateTime StoredDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int OwnerID { get; set; }
        public string Consent { get; set; }
        public string Status { get; set; }
    }
}
