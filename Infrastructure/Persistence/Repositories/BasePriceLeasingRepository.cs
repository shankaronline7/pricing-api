using Pricing.Application.BasePriceLeasing.Queries.BasePriceLeasing;
using Pricing.Application.Common.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace Pricing.Infrastructure.Persistence.Repositories
{
    public class BasePriceLeasingRepository : RepositoryBase<BasePriceLeasingDto>, IBasePriceLeasingRepository
    {
        private readonly ApplicationDbContext _context;
        public BasePriceLeasingRepository(ApplicationDbContext context)
            : base(context)
        {
            {
                _context = context;
            }
        }
        //Trail 0

        public async Task<List<BasePriceLeasingDto>> GetBasePriceLeasing(List<BasePriceLeasingDto> basePriceLeasingDto, CancellationToken cancellationToken)
        {
            // Using Parallel.ForEach to process the list in parallel
            Parallel.ForEach(basePriceLeasingDto, (item) =>
            {
                if (item.Status == "Active" && item.ApprovalStatus == "Approved")
                {
                    item.Status = "Active";
                }
                else if (item.Status == null)
                {
                    item.Status = item.ApprovalStatus;
                }
            

            });
            return await Task.FromResult(basePriceLeasingDto);
        }
        public static string ApplyCultureFormatting(string jsonArray)
        {
            if (string.IsNullOrEmpty(jsonArray))
                return jsonArray;
            var array = JArray.Parse(jsonArray);
            Parallel.ForEach(array, obj =>
            {
                ProcessJsonObject(obj, "DISCOUNT");
                ProcessJsonObject(obj, "MARGIN");
                ProcessJsonObject(obj, "LEASINGRATE");
                ProcessJsonObject(obj, "LEASINGFACTOR");

            });
            // Serialize back to JSON string
            var formattedJson = JsonConvert.SerializeObject(array, Formatting.None)
                                    .Replace("\r", "")
                                   .Replace("\n", "");

            return formattedJson;
        }
        private static void ProcessJsonObject(JToken obj, string propertyName)
        {
            if (obj[propertyName] != null)
            {
                var value = obj[propertyName].Type == JTokenType.Null ? 0 : obj[propertyName].Value<double>();
                obj[propertyName] = value;
            }

        }
    }
}

