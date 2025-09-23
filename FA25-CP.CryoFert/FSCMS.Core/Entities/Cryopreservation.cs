using System;

namespace FSCMS.Core.Entities
{
    /// <summary>
    /// Bản ghi trữ đông
    /// </summary>
    public class Cryopreservation : BaseEntity
    {
        public int SpecimenId { get; set; }
        public DateTime FrozenDate { get; set; }
        public string Method { get; set; } // e.g., Vitrification
        public string? Cryoprotectant { get; set; }
        public string? StrawId { get; set; }
        public int? WitnessUserId { get; set; }
        public string? Notes { get; set; }

        public virtual Specimen? Specimen { get; set; }
        public virtual User? WitnessUser { get; set; }
    }

    /// <summary>
    /// Bản ghi rã đông
    /// </summary>
    public class Thawing : BaseEntity
    {
        public int SpecimenId { get; set; }
        public DateTime ThawDate { get; set; }
        public string? Protocol { get; set; }
        public string Outcome { get; set; } // Survived, Partially, Not survived
        public int? WitnessUserId { get; set; }
        public string? Notes { get; set; }

        public virtual Specimen? Specimen { get; set; }
        public virtual User? WitnessUser { get; set; }
    }
}


