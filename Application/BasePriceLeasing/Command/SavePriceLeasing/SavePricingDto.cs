using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BasePriceLeasing.Command.SavePriceLeasing
{
    
        public class SavePricingDto
        {
            public long? Id { get; set; }

            public string? Brand { get; set; }

            public string? ModelCode { get; set; }

            public long? ModelBaseDataID { get; set; }

            public string? CreateFrom { get; set; }

            public string? LastchangedFrom { get; set; }

            public double? CalculationTarget { get; set; }

            public DateTime? ValidFrom { get; set; }

            public string? Status { get; set; }

            public string? Discounts { get; set; }

            public string? Margins { get; set; }

            public string? Leasingrates { get; set; }

            public string? Leasingfactors { get; set; }

            public long? Term { get; set; }

            public string? ErrorMessage { get; set; }

            public long? ErrorTerm { get; set; }
        }
    }

