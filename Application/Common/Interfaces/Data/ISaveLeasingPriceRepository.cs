using Application.BasePriceLeasing.Command.SavePriceLeasing;


namespace DSP.Pricing.Application.Common.Interfaces.Data
{
    /// <summary>
    /// Interface of SaveLeasingPrice
    /// </summary>
    public interface ISaveLeasingPriceRepository
    {
        /// <summary>
        /// Method signature of SaveLeasingPrice method
        /// </summary>
        /// <param name="saveLeasingPriceDto">List<SaveLeasingPriceDto></param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Returns interger value</returns>
        Task<List<SaveLeasingPriceDto>> SaveLeasingPrice(List<SaveLeasingPriceDto> saveLeasingPriceDto, CancellationToken cancellationToken);
        
        



    }
}
