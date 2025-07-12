namespace ShippingAPI.DTOS.FinancialTransferDtOs
{
    public class SafeTransactionReportDto
    {
        public string SafeName { get; set; } = string.Empty;
        public decimal Credit { get; set; } 
        public decimal Debit { get; set; }
        public string? AdminName { get; set; }
        public string? Note { get; set; }
        public DateTime Date { get; set; }
    }
}
