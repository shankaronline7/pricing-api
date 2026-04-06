using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Pricing.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Application.BasePriceLeasing.Command.SavePriceLeasing
{

    public class SavePricingCommand : IRequest<int>
    {
        public string Status { get; set; }

        public List<SavePricingDto> savePricingDto { get; set; } = new List<SavePricingDto>();
    }

    public class SavePricingCommandHandler : IRequestHandler<SavePricingCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SavePricingCommandHandler> _logger;
        private readonly IMapper _mapper;

        public SavePricingCommandHandler(
            IUnitOfWork unitOfWork,
            ILogger<SavePricingCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int> Handle(SavePricingCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle method request object " + JsonSerializer.Serialize(request));

            if (request?.savePricingDto.Count > 0)
            {
                List<SavePricingDto> savePricingDtos = new List<SavePricingDto>();

                savePricingDtos = await _unitOfWork
                    .savePricingRepository
                    .SavePricing(request.savePricingDto, cancellationToken);

                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}

