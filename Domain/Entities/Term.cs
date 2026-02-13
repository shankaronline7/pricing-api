using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class Term : BaseAuditableEntity
    {
        public long Id { get; set; }

        public int? TermValue { get; set; }   // 12, 24, 36
        public string? Description { get; set; }
        public bool? Default { get; set; }

        // Navigation collection
        public virtual ICollection<TermMileage>? TermMileages { get; set; }
    }
}
