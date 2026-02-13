using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class TermMileage : BaseAuditableEntity
    {
        public long ID { get; set; }
        public long? TermID { get; set; }
        public long? MileageID { get; set; }
        public virtual Term? Term { get; set; }
        public virtual MileageModel? Mileage { get; set; }
        public virtual ICollection<LeasingCalculationResults>? LeasingCalculationResults { get; set; }
        public virtual ICollection<TermMileageSpread>? TermMileageSpreads { get; set; }


    }
}
