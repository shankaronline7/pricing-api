using System;

namespace Application.DTOs
{
    public class BasePriceLeasingPagedDto
    {
        public string BrandName { get; set; }


        public string Series { get; set; }

        public string ModelRange { get; set; }

        public string ModelCode { get; set; }

        public string ModelDescription { get; set; }

        public double? ModelBasePrice { get; set; }

        public double? CalculationTypeValue { get; set; }

        public string Status { get; set; }

        public string ApprovalStatus { get; set; }
        public string Discounts { get; set; }
        public string Margins { get; set; }
        public string LeasingRates { get; set; }
        public string LeasingFactor { get; set; }
    }

}
