using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class PriceGroupDefinition : BaseAuditableEntity
    {
        public long ID { get; set; }
        public string? PGD_Name { get; set; }
        public string? PGD_Description { get; set; }
        public virtual ICollection<LeasingPriceGroupDefination>? LeasingPriceGroupDefinitions { get; set; }

    }
}
