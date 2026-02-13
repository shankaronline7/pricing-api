namespace Pricing.Domain.Entities.FunctionalEntities
{
    public class BasicPriceLeasing
    {
        public long LPC_ID { get; set; }
        public long ModelBaseDataId { get; set; }
        public string? BrandName { get; set; }
        public string? Series { get; set; }
        public string? ModelRange { get; set; }
        public string? SegmentECode { get; set; }

        public string? ModelDescription { get; set; }
        public string? ModelCode { get; set; }
        public string? CalculationTypeValue { get; set; }
        public double? ModelBasePrice { get; set; }
        public string? Term { get; set; }
        public DateTime? LPC_ValidFrom { get; set; }
        public DateTime? LPC_ValidTo { get; set; }
        public string Discounts { get; set; }
        public string? Margins { get; set; }
        public string? Leasingrates { get; set; }
        public string? LeasingFactors { get; set; }
        public string? ApprovalID { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? Status { get; set; }


    }
}
