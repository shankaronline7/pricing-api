using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class FuelTypeModel : BaseAuditableEntity
    {
        public long ID { get; set; }
        public string? FuelType { get; set; }
        public string? HybridVersion { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<ModelBaseData>? ModelBaseDatas { get; set; }
        public virtual ICollection<ModelRangeSegmentAssignment>? ModelRangeSegmentAssignments { get; set; }
    }
}
