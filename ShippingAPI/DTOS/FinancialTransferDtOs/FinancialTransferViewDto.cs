namespace ShippingAPI.DTOS.FinancialTransferDtOs
{
    public class FinancialTransferViewDto
    {
        public string? SourceName { get; set; }          
        public string? DestinationName { get; set; }      
        public decimal Amount { get; set; }
        public string? Note { get; set; }
        public DateTime Date { get; set; }
        public string? AdminName { get; set; }
        public string? TransferDirection { get; set; } // "إضافة" أو "خصم"
    }
}
