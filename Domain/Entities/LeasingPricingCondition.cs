using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class LeasingPricingConditions : BaseAuditableEntity
    {
        public long ID { get; set; }
        public long ModelBaseDataID { get; set; }
        public string? CalculationType { get; set; }
        public double? CalculationTypeValue { get; set; }
        public DateTime? LPC_ValidFrom { get; set; }
        public DateTime? LPC_ValidTo { get; set; }
        public string? Status { get; set; }
        public string? ApprovalID { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? ApprovalRemarks { get; set; }
        public virtual ModelBaseData? ModelBaseData { get; set; }
        public virtual ICollection<LeasingCalculationResults>? LeasingCalculationResults { get; set; }
        public virtual ICollection<LeasingPriceGroupDefination>? LeasingPriceGroupDefinitions { get; set; }
    }
}

