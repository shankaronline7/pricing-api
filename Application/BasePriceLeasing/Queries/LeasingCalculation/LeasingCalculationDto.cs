namespace Application.DTOs
{
    public class LeasingCalculationDto
    {
        public long Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string ModelCode { get; set; } = string.Empty;
        public decimal Discount { get; set; }
        public DateTime ValidFrom { get; set; }
        public int Term { get; set; }
    }
}