using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class RetailCostAssignment : BaseAuditableEntity
    {
        public long ID { get; set; }
        public long? BrandID { get; set; }
        public string? RetailCostType { get; set; }
        public double? RetailCostValue { get; set; }
        public DateTime? RCA_ValidFrom { get; set; }
        public DateTime? RCA_ValidTo { get; set; }
        public string? Status { get; set; }
        public virtual Brand? Brand { get; set; }
    }
}

