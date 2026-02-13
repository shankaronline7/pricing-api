using Pricing.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Pricing.Domain.Entities
{
    public class Segment : BaseAuditableEntity
    {
        [Key]
        public long ID { get; set; }
        public string? SegmentECODE { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<ModelRangeSegmentAssignment>? ModelRangeSegmentAssignments { get; set; }
        public virtual ICollection<TermMileageSpread>? TermMileageSpreads { get; set; }
    }
}
