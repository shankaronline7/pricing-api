using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BasePriceLeasing.Queries.LeasingPricingConditionsAudit
{
    public class LeasingAuditDto
    {
        [Column("LPC_Audit_ID")]
        public long LpcAuditId { get; set; }
        public long ID { get; set; }
        public long ModelBaseDataID { get; set; }
        public string Operation { get; set; }

        public string CalculationType { get; set; }
        public double? CalculationTypeValue { get; set; }

        public DateTime? LPC_ValidFrom { get; set; }
        public DateTime? LPC_ValidTo { get; set; }

        public string created_by { get; set; }
        public DateTime created_date { get; set; }

        public string updated_by { get; set; }
        public DateTime? updated_date { get; set; }

        public string Status { get; set; }

        public string ApprovalID { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalRemarks { get; set; }
    }
}
