using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class LeasingCalculationResults : BaseAuditableEntity
    {
        public long ID { get; set; }
        public long LeasingPricingConditionsID { get; set; }
        public double? Margin { get; set; }
        public double? LeasingRate { get; set; }
        public double? LeasingFactor { get; set; }
        public double? LeasingDiscount { get; set; }
        public long? TermMileageID { get; set; }
        public string? ErrorMessage { get; set; }
        public virtual TermMileage? TermMileage { get; set; }
        public virtual LeasingPricingConditions? LeasingPricingCondition { get; set; }

    }
}
