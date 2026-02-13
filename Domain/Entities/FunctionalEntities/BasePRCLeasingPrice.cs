namespace Pricing.Domain.Entities.FunctionalEntities
{
    public class BasePRCLeasingPrice
    {

        public int Id { get; set; }

        public string? Pricingid { get; set; }

        public string? Pricesheetid { get; set; }

        public string? Vehiclebaseid { get; set; }

        public DateTime? Createat { get; set; }

        public string? Createfrom { get; set; }

        public DateTime? Lastchangedat { get; set; }

        public string? Lastchangedfrom { get; set; }

        public string? Calculationtype { get; set; }

        public double? Calculationtarget { get; set; }

        public double? Calculationpitch { get; set; }

        public int? Pricingchanged { get; set; }

        public string? Provide { get; set; }

        public string? Pricegroups { get; set; }

        public string? Leasingrates { get; set; }

        public string? Discounts { get; set; }

        public string? Margins { get; set; }

        public string? Status { get; set; }

        public bool? Coapproval { get; set; }

        public string? Coid { get; set; }

        public bool? Fbapproval { get; set; }

        public string? Fbid { get; set; }

        public DateTime? ValidFrom { get; set; }

        public int? Lprcid { get; set; }
    }
}
