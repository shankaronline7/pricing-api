using Pricing.Domain.Common;

namespace Pricing.Domain.Entities
{
    public class ModelBaseData : BaseAuditableEntity
    {
        public long ID { get; set; }
        public long? BrandID { get; set; }
        public long? ModelRangeID { get; set; }
        public long? SeriesID { get; set; }
        public long? ModelDescriptionID { get; set; }
        public long? FuelTypeID { get; set; }
        public string? VehicleBody { get; set; }
        public string? ModelCode { get; set; }
        public string? CountryPlant { get; set; }
        public DateTime? SOC { get; set; }
        public DateTime? SOP { get; set; }
        public DateTime? EOP { get; set; }
        public DateTime? SOC_ValidFrom { get; set; }
        public DateTime? EOP_ValidTo { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual ModelRangeModel? ModelRange { get; set; }
        public virtual SeriesModel? Series { get; set; }
        public virtual ModelDescriptionModel? ModelDescription { get; set; }
        public virtual FuelTypeModel? FuelType { get; set; }
        public virtual ICollection<LeasingPricingConditions>? LeasingPricingConditions { get; set; }
        public virtual ICollection<ModelPriceData>? ModelPriceDatas { get; set; }

    }
}
