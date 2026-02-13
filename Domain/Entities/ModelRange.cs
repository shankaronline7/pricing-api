using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class ModelRangeModel : BaseAuditableEntity
    {
        public long ID { get; set; }
        public string? ModelRange { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<ModelBaseData>? ModelBaseDatas { get; set; }
        public virtual ICollection<ModelRangeSegmentAssignment>? ModelRangeSegmentAssignments { get; set; }
        public virtual ICollection<ModelRangeSeriesAssignment>? ModelRangeSeriesAssignments { get; set; }
    }
}
