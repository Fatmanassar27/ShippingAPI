namespace ShippingAPI.DTOS.FinancialTransferDtOs
{
    public class BankTransactionReportDto
    {
        public string BankName { get; set; } = string.Empty;

        public decimal Credit { get; set; }        // ← المديونية (الفلوس اللي دخلت البنك)
        public decimal Debit { get; set; }         // ← الرصيد (الفلوس اللي اتسحبت من البنك)

        public string? Note { get; set; }          // ← الملحوظة
        public DateTime Date { get; set; }         // ← تاريخ العملية
    }
}
