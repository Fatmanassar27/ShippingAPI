using Microsoft.EntityFrameworkCore;
using ShippingAPI.Data;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class FinancialTransferRepo : GenericRepo<FinancialTransfer>
    {
        public FinancialTransferRepo(ShippingContext db) : base(db)
        {
        }


        public async Task<bool> AddTransferAsync(FinancialTransfer entity)
        {
            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                await db.FinancialTransfers.AddAsync(entity);

                if (entity.SourceBankId.HasValue && entity.SourceBankId > 0)
                {
                    var sourceBank = await db.Banks.FindAsync(entity.SourceBankId.Value);
                    if (sourceBank == null) throw new Exception("Source bank not found.");
                    sourceBank.Balance -= entity.Amount;
                }
                else if (entity.SourceSafeId.HasValue && entity.SourceSafeId > 0)
                {
                    var sourceSafe = await db.Safes.FindAsync(entity.SourceSafeId.Value);
                    if (sourceSafe == null) throw new Exception("Source safe not found.");
                    sourceSafe.Balance -= entity.Amount;
                }

                if (entity.DestinationBankId.HasValue && entity.DestinationBankId > 0)
                {
                    var destBank = await db.Banks.FindAsync(entity.DestinationBankId.Value);
                    if (destBank == null) throw new Exception("Destination bank not found.");
                    destBank.Balance += entity.Amount;
                }
                else if (entity.DestinationSafeId.HasValue && entity.DestinationSafeId > 0)
                {
                    var destSafe = await db.Safes.FindAsync(entity.DestinationSafeId.Value);
                    if (destSafe == null) throw new Exception("Destination safe not found.");
                    destSafe.Balance += entity.Amount;
                }

                await db.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
        public List<FinancialTransfer> GetBankTransfersFiltered( string? bankName = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var transfers = db.FinancialTransfers.Include(t => t.SourceBank)
                .Include(t => t.DestinationBank)
                .Include(t => t.Admin)
                .Where(t => t.SourceBankId > 0 || t.DestinationBankId > 0)
                .AsQueryable();

            if (!string.IsNullOrEmpty(bankName))
            {
                transfers = transfers.Where(t =>
                    (t.SourceBank != null && t.SourceBank.Name.Contains(bankName)) ||
                    (t.DestinationBank != null && t.DestinationBank.Name.Contains(bankName))
                );
            }

            if (startDate.HasValue)
                transfers = transfers.Where(t => t.Date >= startDate.Value);

            if (endDate.HasValue)
                transfers = transfers.Where(t => t.Date <= endDate.Value);

            return transfers.OrderByDescending(t => t.Date).ToList();
        }

        public List<FinancialTransfer> GetSafeTransfersFiltered(string? safeName = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var transfers = db.FinancialTransfers
                .Include(t => t.SourceSafe)
                .Include(t => t.DestinationSafe)
                .Include(t => t.Admin)
                .Where(t => t.SourceSafeId > 0 || t.DestinationSafeId > 0)
                .AsQueryable();

            if (!string.IsNullOrEmpty(safeName))
            {
                transfers = transfers.Where(t =>
                    (t.SourceSafe != null && t.SourceSafe.Name.Contains(safeName)) ||
                    (t.DestinationSafe != null && t.DestinationSafe.Name.Contains(safeName))
                );
            }

            if (startDate.HasValue)
                transfers = transfers.Where(t => t.Date >= startDate.Value);

            if (endDate.HasValue)
                transfers = transfers.Where(t => t.Date <= endDate.Value);

            return transfers.OrderByDescending(t => t.Date).ToList();
        }

    }
}
