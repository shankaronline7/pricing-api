namespace Pricing.Domain.Constants
{
    public static class WorkflowStatus
    {
        public const string InWork = "InWork";
        public const string InApproval = "InApproval";
        public const string Approved = "Approved";
        public const string WithDrawn = "WithDrawn";
        public const string Declined = "Declined";
        public const string WFStatusChanged = "ChangeStatus";
        public const string Binding = "Binding";
    }
}