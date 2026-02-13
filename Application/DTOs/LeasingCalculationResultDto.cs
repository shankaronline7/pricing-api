
namespace Application.DTOs
{
    public class LeasingCalculationResultDto
    {
        // Header fields
        public long Id { get; set; }
        public long ModelBaseDataId { get; set; }

        public string Brand { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;
        public string ModelRange { get; set; } = string.Empty;
        public string ModelDescription { get; set; } = string.Empty;
        public string ModelCode { get; set; } = string.Empty;

        public string CalculationTypeValue { get; set; } = string.Empty;

        public int Term { get; set; }

        // 🔴 CHANGED: DateTime → string (to match ToString())
        public string ValidFrom { get; set; } = string.Empty;
        public string? ValidTo { get; set; }

        // Calculated JSON fields
        public string Discounts { get; set; } = string.Empty;
        public string Margins { get; set; } = string.Empty;

        // 🔴 CHANGED: Property name casing to match assignment
        public string Leasingrates { get; set; } = string.Empty;
        public string LeasingFactor { get; set; } = string.Empty;

        // Workflow fields
        public long? ApprovalID { get; set; }
        public string? Status { get; set; }
        public string? ErrorMessage { get; set; }

       
    }
}
