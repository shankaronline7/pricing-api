using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class ModelDescriptionModel : BaseAuditableEntity
    {
        public long ID { get; set; }
        public string? ModelDescription { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<ModelBaseData>? ModelBaseDatas { get; set; }
    }
}
