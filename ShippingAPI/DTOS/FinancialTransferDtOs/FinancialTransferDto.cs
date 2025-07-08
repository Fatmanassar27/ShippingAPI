namespace ShippingAPI.DTOS.FinancialTransferDtOs
{
    public class FinancialTransferDto
    {
        public int? SourceBankId { get; set; }
        public int? SourceSafeId { get; set; }

        public int? DestinationBankId { get; set; }
        public int? DestinationSafeId { get; set; }

        public decimal Amount { get; set; }
        public string? Note { get; set; }
    }
}
