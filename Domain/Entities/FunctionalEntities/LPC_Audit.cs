using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FunctionalEntities
{
    public class LPC_Audit
    {
        public long LPC_Audit_ID { get; set; }
        public long ID { get; set; }
        public long ModelBaseDataID { get; set; }

        public string? Operation { get; set; }
        public string? CalculationType { get; set; }
        public double? CalculationTypeValue { get; set; }

        public DateTime? LPC_ValidFrom { get; set; }
        public DateTime? LPC_ValidTo { get; set; }

        public string? created_by { get; set; }
        public DateTime created_date { get; set; }

        public string? updated_by { get; set; }
        public DateTime? updated_date { get; set; }

        public string? Status { get; set; }

        public string? ApprovalID { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? ApprovalRemarks { get; set; }
    }
}
