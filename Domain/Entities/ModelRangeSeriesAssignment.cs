using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class ModelRangeSeriesAssignment : BaseAuditableEntity
    {
        public long ID { get; set; }
        public long? ModelRangeID { get; set; }
        public long? SeriesID { get; set; }
        public DateTime? MSA_ValidFrom { get; set; }
        public DateTime? MSA_ValidTo { get; set; }
        public string? Status { get; set; }
        public virtual ModelRangeModel? ModelRange { get; set; }
        public virtual SeriesModel? Series { get; set; }


    }
}
