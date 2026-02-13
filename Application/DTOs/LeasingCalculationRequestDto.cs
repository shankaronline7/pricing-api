namespace Application.DTOs
{
    public class LeasingCalculationRequestDto
    {
        public long Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string ModelCode { get; set; } = string.Empty;
        public decimal Discount { get; set; }
        public DateTime ValidFrom { get; set; }
        public int Term { get; set; }
        public long ModelBaseDataId { get; internal set; }
        public string Series { get; internal set; }
        public string ModelRange { get; internal set; }
        public string ModelDescription { get; internal set; }
        public string CalculationTypeValue { get; internal set; }
    }
}
