using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class TermMileageSpread : BaseAuditableEntity
    {
        public long ID { get; set; }
        public long? SegmentID { get; set; }
        public double? SpreadValue { get; set; }
        public DateTime? TMS_ValidFrom { get; set; }
        public DateTime? TMS_ValidTo { get; set; }
        public long? TermMileageID { get; set; }
        public string? Status { get; set; }
        public virtual TermMileage? TermMileage { get; set; }
        public virtual Segment? Segment { get; set; }
    }
}
