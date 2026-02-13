

using AutoMapper;
using Pricing.Domain.Entities.FunctionalEntities;

namespace Pricing.Application.BasePriceLeasing.Queries.BasePriceLeasing;

/// <summary>
/// This schema used send response to caller
/// </summary>
public class BasePriceLeasingDto

{
    public long ID { get; set; }
    public long ModelBaseDataId { get; set; }
    public string? Brand { get; set; }
    public string? Series { get; set; }
    public string? ModelRange { get; set; }
    public string? SegmentECode { get; set; }
    public string? ModelDescription { get; set; }
    public string? ModelCode { get; set; }
    public double? CalculationTypeValue { get; set; }
    public double? NetListPrice { get; set; }
    public string? Term { get; set; }
    public string? ValidFrom { get; set; }
    public string? ValidTo { get; set; }
    public string Discounts { get; set; }
    public string? Margins { get; set; }
    public string? Leasingrates { get; set; }
    public string? LeasingFactor { get; set; }
    public string? ApprovalID { get; set; }
    public string? ApprovalStatus { get; set; }
    public string? Status { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<BasicPriceLeasing, BasePriceLeasingDto>()
                 .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.LPC_ID))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.BrandName))
              .ForMember(dest => dest.ValidFrom, opt => opt.MapFrom(src => src.LPC_ValidFrom))
              .ForMember(dest => dest.ValidTo, opt => opt.MapFrom(src => src.LPC_ValidTo))
              .ForMember(dest => dest.LeasingFactor, opt => opt.MapFrom(src => src.LeasingFactors))
            .ForMember(dest => dest.NetListPrice, opt => opt.MapFrom(src => src.ModelBasePrice));
        }
    }
}
public class Discount
{
    public int MILEAGE { get; set; }
    public double DISCOUNT { get; set; }
}
public class Margin
{
    public int MILEAGE { get; set; }
    public double MARGIN { get; set; }
}
public class LeasingRate
{
    public int MILEAGE { get; set; }
    public double LEASINGRATE { get; set; }
}
public class LeasingFactor
{
    public int MILEAGE { get; set; }
    public double LEASINGFACTOR { get; set; }
}