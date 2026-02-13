using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class ModelPriceData : BaseAuditableEntity
    {
        public long ID { get; set; }
        public long ModelBaseDataID { get; set; }
        public double ModelBasePrice { get; set; }
        public DateTime? MPD_ValidFrom { get; set; }
        public DateTime? MPD_ValidTo { get; set; }
        public string? Status { get; set; }
        public virtual ModelBaseData? ModelBaseData { get; set; }

    }
}
