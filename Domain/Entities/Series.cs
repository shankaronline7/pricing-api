using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class SeriesModel : BaseAuditableEntity
    {
        public long ID { get; set; }
        public string? Series { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<ModelBaseData>? ModelBaseDatas { get; set; }
        public virtual ICollection<ModelRangeSeriesAssignment>? ModelRangeSeriesAssignments { get; set; }
    }
}
