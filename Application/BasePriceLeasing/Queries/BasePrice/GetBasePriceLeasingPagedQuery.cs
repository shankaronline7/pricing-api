using Application.DTOs;
using MediatR;
using Pricing.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BasePriceLeasing.Queries.BasePrice
{
    public class GetBasePriceLeasingPagedQuery
          : IRequest<List<BasePriceLeasingPagedDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetBasePriceLeasingPagedQueryHandler
        : IRequestHandler<GetBasePriceLeasingPagedQuery, List<BasePriceLeasingPagedDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBasePriceLeasingPagedQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BasePriceLeasingPagedDto>> Handle(
            GetBasePriceLeasingPagedQuery request,
            CancellationToken cancellationToken)
        {
            var parameters = new
            {
                p_page_number = request.PageNumber,
                p_page_size = request.PageSize
            };

            string functionName =
    "SELECT * FROM fn_get_modelbaseprice_paginated(@p_page_number, @p_page_size)";

            var result =
                await _unitOfWork.ExecFunctionWithParmsAsync<BasePriceLeasingPagedDto>(
                    functionName,
                    parameters);

            return result;
        }
    }
}
