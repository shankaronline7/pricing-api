using AutoMapper;
using Pricing.Domain.Entities.FunctionalEntities;

namespace DSP.Pricing.Application.BasePriceLeasing.Queries.EditLeasing;

public class EditBasePriceLeasingDto
{

    public long ID { get; set; }
    public long ModelBaseDataId { get; set; }
    public string? Brand { get; set; }
    public string? Series { get; set; }
    public string? ModelRange { get; set; }
    public string? SegmentECode { get; set; }
    public string? ModelDescription { get; set; }
    public string? ModelCode { get; set; }
    public string? CalculationTypeValue { get; set; }
    public string? Term { get; set; }
    public string? ValidFrom { get; set; }
    public string? ValidTo { get; set; }
    public string? Discounts { get; set; }
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
            CreateMap<BasicPriceLeasing, EditBasePriceLeasingDto>()
                 .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.LPC_ID))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.BrandName))
                .ForMember(dest => dest.ValidFrom, opt => opt.MapFrom(src => src.LPC_ValidFrom))
                .ForMember(dest => dest.ValidTo, opt => opt.MapFrom(src => src.LPC_ValidTo))
                .ForMember(dest => dest.LeasingFactor, opt => opt.MapFrom(src => src.LeasingFactors));

        }
    }

}