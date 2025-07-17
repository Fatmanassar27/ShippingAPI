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
                entity.Date = DateTime.Now;

                bool isSafeToSafe = entity.SourceSafeId != null && entity.DestinationSafeId != null;
                bool isBankToBank = entity.SourceBankId != null && entity.DestinationBankId != null;

                if (isSafeToSafe)
                {
                    var sourceSafe = await db.Safes.FindAsync(entity.SourceSafeId.Value);
                    var destSafe = await db.Safes.FindAsync(entity.DestinationSafeId.Value);

                    if (sourceSafe == null || destSafe == null)
                        throw new Exception("Safe not found.");

                    if (sourceSafe.Balance < entity.Amount)
                        throw new Exception("Insufficient balance in source safe.");

                    sourceSafe.Balance -= entity.Amount;
                    destSafe.Balance += entity.Amount;

                    var fromSafe = new FinancialTransfer
                    {
                        SourceSafeId = entity.SourceSafeId,
                        Amount = entity.Amount,
                        Note = $"Transfer to Safe {entity.DestinationSafeId}",
                        AdminId = entity.AdminId,
                        Date = entity.Date
                    };

                    var toSafe = new FinancialTransfer
                    {
                        DestinationSafeId = entity.DestinationSafeId,
                        Amount = entity.Amount,
                        Note = $"Transfer from Safe {entity.SourceSafeId}",
                        AdminId = entity.AdminId,
                        Date = entity.Date
                    };

                    await db.FinancialTransfers.AddRangeAsync(fromSafe, toSafe);
                }
                else if (isBankToBank)
                {
                    var sourceBank = await db.Banks.FindAsync(entity.SourceBankId.Value);
                    var destBank = await db.Banks.FindAsync(entity.DestinationBankId.Value);

                    if (sourceBank == null || destBank == null)
                        throw new Exception("Bank not found.");

                    if (sourceBank.Balance < entity.Amount)
                        throw new Exception("Insufficient balance in source bank.");

                    sourceBank.Balance -= entity.Amount;
                    destBank.Balance += entity.Amount;

                    var fromBank = new FinancialTransfer
                    {
                        SourceBankId = entity.SourceBankId,
                        Amount = entity.Amount,
                        Note = $"Transfer to Bank {entity.DestinationBankId}",
                        AdminId = entity.AdminId,
                        Date = entity.Date
                    };

                    var toBank = new FinancialTransfer
                    {
                        DestinationBankId = entity.DestinationBankId,
                        Amount = entity.Amount,
                        Note = $"Transfer from Bank {entity.SourceBankId}",
                        AdminId = entity.AdminId,
                        Date = entity.Date
                    };

                    await db.FinancialTransfers.AddRangeAsync(fromBank, toBank);
                }
                else
                {
                    if (entity.SourceSafeId.HasValue)
                    {
                        var sourceSafe = await db.Safes.FindAsync(entity.SourceSafeId.Value);
                        if (sourceSafe == null) throw new Exception("Source safe not found.");
                        if (sourceSafe.Balance < entity.Amount) throw new Exception("Insufficient balance.");
                        sourceSafe.Balance -= entity.Amount;
                    }

                    if (entity.DestinationSafeId.HasValue)
                    {
                        var destSafe = await db.Safes.FindAsync(entity.DestinationSafeId.Value);
                        if (destSafe == null) throw new Exception("Destination safe not found.");
                        destSafe.Balance += entity.Amount;
                    }

                    if (entity.SourceBankId.HasValue)
                    {
                        var sourceBank = await db.Banks.FindAsync(entity.SourceBankId.Value);
                        if (sourceBank == null) throw new Exception("Source bank not found.");
                        if (sourceBank.Balance < entity.Amount) throw new Exception("Insufficient balance.");
                        sourceBank.Balance -= entity.Amount;
                    }

                    if (entity.DestinationBankId.HasValue)
                    {
                        var destBank = await db.Banks.FindAsync(entity.DestinationBankId.Value);
                        if (destBank == null) throw new Exception("Destination bank not found.");
                        destBank.Balance += entity.Amount;
                    }

                    await db.FinancialTransfers.AddAsync(entity);
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


        public List<FinancialTransfer> GetBankTransfersFiltered(string? bankName = null, DateTime? startDate = null, DateTime? endDate = null)
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
