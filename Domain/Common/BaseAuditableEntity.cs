using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pricing.Domain.Common
{
    public abstract class BaseAuditableEntity :IBaseAuditableEntity
    {
        [Column("created_date")]
        public virtual DateTime? CreatedOn { get; set; }

        [Column("created_by")]
        public virtual string? CreatedBy { get; set; }

        [Column("updated_date")]
        public virtual DateTime? UpdatedOn { get; set; }

        [Column("updated_by")]
        public virtual string? UpdatedBy { get; set; }
    }
}
