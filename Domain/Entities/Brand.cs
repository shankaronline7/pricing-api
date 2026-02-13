using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{

    public class Brand : BaseAuditableEntity
    {
        public long ID { get; set; }
        public string? BrandName { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<ModelBaseData>? ModelBaseDatas { get; set; }
        public virtual ICollection<RetailCostAssignment>? RetailCostAssignments { get; set; }
    }
}
