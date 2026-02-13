using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class MileageModel : BaseAuditableEntity
    {
        public long ID { get; set; }
        public int? MileageValue { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<TermMileage>? TermMileages { get; set; }


        //public int AnnualMileage { get; set; }
    }
}
