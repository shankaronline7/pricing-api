using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IBaseAuditableEntity
    {
        DateTime? CreatedOn { get; set; }
        string? CreatedBy { get; set; }
        DateTime? UpdatedOn { get; set; }
        string? UpdatedBy { get; set; }
    }
}
