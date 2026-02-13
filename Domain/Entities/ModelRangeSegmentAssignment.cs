using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class ModelRangeSegmentAssignment : BaseAuditableEntity
    {
        public long ID { get; set; }
        public long? SegmentID { get; set; }
        public long? OriginalSegmentID { get; set; }
        public long ModelRangeID { get; set; }
        public long FuelTypeID { get; set; }
        public DateTime? MSA_ValidFrom { get; set; }
        public DateTime? MSA_ValdiTo { get; set; }
        public string? Status { get; set; }
        public virtual Segment? Segment { get; set; }
        public virtual ModelRangeModel? ModelRange { get; set; }
        public virtual FuelTypeModel? FuelType { get; set; }
        public virtual OriginalSegment? OriginalSegment { get; set; }



    }
}
