using Application.BasePriceLeasing.Command.SavePriceLeasing;
using AutoMapper;
using Pricing.Domain.Constants;
using MediatR;
using Pricing.Application.Common.Interfaces;
using System.Data;
using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace DSP.Pricing.Application.BasePriceLeasing.Command.SavePriceLeasing
{
    /// <summary>
    /// Command class
    /// </summary>
    public class SaveLeasingPriceCommand : IRequest<int>
    {
        /// <summary>
        /// Status of the approval (IN Work/In Approval/Approved/Active/History) 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Input object with vehicle details to save
        /// </summary>
        public List<SaveLeasingPriceDto> saveLeasingPriceDto { get; set; } = new List<SaveLeasingPriceDto>();

    }

    /// <summary>
    /// Command hanler
    /// </summary>
    public class SaveLeasingPricecommandHandler : IRequestHandler<SaveLeasingPriceCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SaveLeasingPricecommandHandler> _logger;
        private readonly IMapper _mapper;
        


        /// <summary>
        /// Parameterized constructore of command handler
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger<SaveLeasingPricecommandHandler></param>
        /// <param name="mapper">IMapper</param>
        public SaveLeasingPricecommandHandler(IUnitOfWork unitOfWork, ILogger<SaveLeasingPricecommandHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
           

        }

        /// <summary>
        /// Handel method
        /// </summary>
        /// <param name="request">SaveLeasingPrice</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>int</returns>
        public async Task<int> Handle(SaveLeasingPriceCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Handle method request object " + System.Text.Json.JsonSerializer.Serialize(request));
            if (request?.saveLeasingPriceDto.Count > 0)
            {
                List<SaveLeasingPriceDto> saveLeasingPriceDtos = new List<SaveLeasingPriceDto>();
                saveLeasingPriceDtos = await _unitOfWork.saveLeasingPriceRepository.SaveLeasingPrice(request.saveLeasingPriceDto, cancellationToken);
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}