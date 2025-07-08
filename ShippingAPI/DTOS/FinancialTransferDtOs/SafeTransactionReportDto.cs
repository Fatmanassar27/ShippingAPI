namespace ShippingAPI.DTOS.FinancialTransferDtOs
{
    public class SafeTransactionReportDto
    {
        public string SafeName { get; set; } = string.Empty;

        public decimal Credit { get; set; } // المديونية - فلوس دخلت الخزنة
        public decimal Debit { get; set; }  // الرصيد - فلوس خرجت من الخزنة

        public string? Note { get; set; }
        public DateTime Date { get; set; }
    }
}
