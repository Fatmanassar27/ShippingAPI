using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShippingAPI.Models;

namespace ShippingAPI.Data
{
    public class ShippingContext: IdentityDbContext<ApplicationUser>
    {
        public ShippingContext()
        {
            
        }
        public ShippingContext(DbContextOptions<ShippingContext> options)
            : base(options)
        {
        }
        public DbSet<Branch> Branches { get; set; } 
        public DbSet<AccountTransaction> AccountTransactions { get; set; } 
        public DbSet<City> Cities { get; set; } 
        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderItem> OrderItems { get; set; } 
        public DbSet<Bank> Banks { get; set; } 
        public DbSet<CustomPrice> CustomPrices { get; set; }
        public DbSet<ExtraVillagePrice> ExtraVillagePrice { get; set; }
        public DbSet<FinancialTransfer> FinancialTransfers { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RejectionReason> RejectionReasons { get; set; }
        public DbSet<Safe> Safes { get; set; }
        public DbSet<ShippingType> ShippingTypes { get; set; }
        public DbSet<AdminGroup> AdminGroups { get; set; }
        public DbSet<AdminProfile> AdminProfiles { get; set; }
        public DbSet<AdminGroupPermission> AdminGroupPermissions { get; set; }
        public DbSet<Weight> Weights { get; set; }
        public DbSet<TraderProfile> TraderProfiles { get; set; }
        public DbSet<CourierProfile> CourierProfiles { get; set; }
        public DbSet<CourierGovernorate> CourierGovernorates { get; set; }
        public DbSet<CourierBranch> CourierBranches { get; set; }
        public DbSet<PermissionAction> PermissionActions { get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
    }
}
