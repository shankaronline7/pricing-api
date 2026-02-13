using DSP.Pricing.Application.BasePriceLeasing.Queries.EditLeasing;

namespace DSP.Pricing.Application.Common.Interfaces.Data
{
    public interface IBaseEditPriceLeasingRepository
    {
        /// <summary>
        /// Get list of Edit Base Price Leasing records with Active,New In work, In Approval, Approved,Declined status based on LPC_Id
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<EditBasePriceLeasingDto>> GetEditBasePriceLeasing(List<EditBasePriceLeasingDto> editBasePriceLeasingDto, CancellationToken cancellationToken);
    }
}
