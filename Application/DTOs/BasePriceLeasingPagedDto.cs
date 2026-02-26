using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BasePriceLeasingPagedDto
    {
        public string BrandName { get; set; }
        public string Series { get; set; }
        public string ModelRange { get; set; }
        public string ModelCode { get; set; }
        public string ModelDescription { get; set; }
        public double ModelBasePrice { get; set; }
    }
}
