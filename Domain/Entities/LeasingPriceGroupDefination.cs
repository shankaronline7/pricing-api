using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class LeasingPriceGroupDefination : BaseAuditableEntity
    {
        public long ID { get; set; }
        public long? PriceGroupDefinitionID { get; set; }
        public long? LeasingPricingConditionsID { get; set; }
        public virtual PriceGroupDefinition? PriceGroupDefinition { get; set; }
        public virtual LeasingPricingConditions? LeasingPricingCondition { get; set; }


    }
}
