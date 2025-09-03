using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    public class Patient : BaseEntity
    {
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string NationalID { get; set; }
        public string MedicalHistory { get; set; }
        public string Allergies { get; set; }
        public string Status { get; set; }
    }
}
