using FSCMS.Core.Models.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Entities
{
    public class HospitalData : BaseEntity<Guid>
    {
        public HospitalData() { }

        public HospitalData(
            Guid id,
            decimal value
            )
        {
            Id = id;
            Value = value;
        }
        public decimal Value { get; set; }
    }
}
