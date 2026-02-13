namespace Application.BasePriceLeasing.Command.SavePriceLeasing
{
    /// <summary>
    /// Input class of SaveLeasingPrice
    /// </summary>
    public class SaveLeasingPriceDto
    {
        /// <summary>
        /// Unique Id of the leasing claculations
        /// </summary>
        public long? Id { get; set; }
        /// <summary>
        /// Brand
        /// </summary>
        public string? Brand { get; set; }
        /// <summary>
        /// ModelCode
        /// </summary>
        public string? ModelCode { get; set; }
        /// <summary>
        /// Unique Id of the leasing claculations
        /// </summary>
        public long? ModelBaseDataID { get; set; }

        /// <summary>
        /// Unique Id of the leasing price calculation
        /// </summary>

        /// <summary>
        /// Name of the user who creates the calculation
        /// </summary>
        public string? CreateFrom { get; set; }

        /// <summary>
        /// Name of the user who updated the calculations recently
        /// </summary>
        public string? LastchangedFrom { get; set; }


        /// <summary>
        /// Target value to calculate the vehicle price details
        /// </summary>
        public double? CalculationTarget { get; set; }

        /// <summary>
        /// ValidFrom- date from when the leasing price will be active
        /// </summary>
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// Status of the approval (IN Work/In Approval/Approved/Active/History)
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Discounts of the vehicle for all the mileages
        /// </summary>
        public string? Discounts { get; set; }

        /// <summary>
        /// Margin value of the vehicle for all the milages
        /// </summary>
        public string? Margins { get; set; }

        /// <summary>
        /// leasing rate of the vehicle for all the mileages
        /// </summary>
        public string? Leasingrates { get; set; }
        /// <summary>
        /// Leasing factors of the vehicle for all the mileages
        /// </summary>
        public string? Leasingfactors { get; set; }
        public long? Term { get; set; }
        public string? ErrorMessage { get; set; }
        public long? ErrorTerm { get; set; }

    }
}
