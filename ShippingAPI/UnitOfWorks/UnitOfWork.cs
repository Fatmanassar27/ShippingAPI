using ShippingAPI.Data;
using ShippingAPI.Models;
using ShippingAPI.Repositories;

namespace ShippingAPI.UnitOfWorks
{
    public class UnitOfWork
    {
        ShippingContext context;

        public AccountTransactionRepo accountTransactionRepo;
        public AdminGroupRepo adminGroupRepo;
        public AdminGroupPermissionRepo adminGroupPermissionRepo;
        public AdminProfileRepo adminProfileRepo;
        public ApplicationUserRepo applicationUserRepo;
        public BankRepo bankRepo;
        public BranchRepo branchRepo;
        public CityRepo cityRepo;
        public CourierBranchRepo courierBranchRepo;
        public CourierGovernorateRepo courierGovernorateRepo;
        public CourierProfileRepo courierProfileRepo;
        public CustomPriceRepo customPriceRepo;
        public ExtraVillagePriceRepo extraVillagePriceRepo;
        public FinancialTransferRepo financialTransferRepo;
        public GovernateRepo governateRepo;
        public OrderRepo orderRepo;
        public OrderItemRepo orderItemRepo;
        public PermissionRepo permissionRepo;
        public RejectionReasonRepo rejectionReasonRepo;
        public SafeRepo safeRepo;
        public ShippingTypeRepo shippingTypeRepo;
        public TraderProfileRepo traderProfileRepo;
        public WeightRepo weightRepo;
        public PermissionActionRepo permissionActionRepo;
        public UserPermissionRepo userPermissionRepo;
        public ActionPermissionRepo actionPermissionRepo;

        public UnitOfWork(ShippingContext context) {
            this.context = context;
        }

        public AccountTransactionRepo AccountTransactionRepo
        {
            get
            {
                if (accountTransactionRepo == null)
                    accountTransactionRepo = new AccountTransactionRepo(context);
                return accountTransactionRepo;
                
            }
        }
        public AdminGroupRepo AdminGroupRepo
        {
            get { 
                if(adminGroupRepo == null)
                    adminGroupRepo = new AdminGroupRepo(context);
                return adminGroupRepo;
            }
        }

        public AdminGroupPermissionRepo AdminGroupPermissionRepo
        {
            get
            {
                if (adminGroupPermissionRepo == null)
                    adminGroupPermissionRepo = new AdminGroupPermissionRepo(context);
                return adminGroupPermissionRepo;
            }
        }

        public AdminProfileRepo AdminProfileRepo
        {
            get
            {
                if (adminProfileRepo == null)
                    adminProfileRepo = new AdminProfileRepo(context);
                return adminProfileRepo;
            }
        }
        public ApplicationUserRepo ApplicationUserRepo
        {
            get
            {
                if (applicationUserRepo == null)
                    applicationUserRepo = new ApplicationUserRepo(context);
                return applicationUserRepo;
            }
        }
        public BankRepo BankRepo
        {
            get
            {
                if (bankRepo == null)
                    bankRepo = new BankRepo(context);
                return bankRepo;

            }
        }

        public BranchRepo BranchRepo
        {
            get
            {
                if (branchRepo == null)
                    branchRepo = new BranchRepo(context);
                return branchRepo;
            }
        }

        public CityRepo CityRepo
        {
            get
            {
                if (cityRepo == null)
                    cityRepo = new CityRepo(context);
                return cityRepo;
            }
        }
        public CourierBranchRepo CourierBranchRepo
        {
            get
            {
                if(courierBranchRepo == null)
                    courierBranchRepo = new CourierBranchRepo(context);
                return courierBranchRepo;
            }
        }
        public CourierGovernorateRepo CourierGovernorateRepo
        {
            get
            {
                if (courierGovernorateRepo == null)
                    courierGovernorateRepo = new CourierGovernorateRepo(context);
                return courierGovernorateRepo;
            }
        }
        public CourierProfileRepo CourierProfileRepo
        {
            get
            {
                if (courierProfileRepo == null)
                    courierProfileRepo = new CourierProfileRepo(context);
                return courierProfileRepo;
            }
        }
        public CustomPriceRepo CustomPriceRepo
        {
            get
            {
                if (customPriceRepo == null)
                    customPriceRepo = new CustomPriceRepo(context);
                return customPriceRepo;
            }
        }

        public ExtraVillagePriceRepo ExtraVillagePriceRepo
        {
            get
            {
                if (extraVillagePriceRepo == null)
                    extraVillagePriceRepo = new ExtraVillagePriceRepo(context);
                return extraVillagePriceRepo;
            }
        }

        public FinancialTransferRepo FinancialTransferRepo
        {
            get
            {
                if (financialTransferRepo == null)
                    financialTransferRepo = new FinancialTransferRepo(context);
                return financialTransferRepo;
            }
        }

        public GovernateRepo GovernateRepo
        {
            get
            {
                if (governateRepo == null)
                    governateRepo = new GovernateRepo(context);
                return governateRepo;
            }
        }

        public OrderRepo OrderRepo
        {
            get
            {
                if (orderRepo == null)
                    orderRepo = new OrderRepo(context);
                return orderRepo;
            }
        }
        public OrderItemRepo OrderItemRepo
        {
            get
            {
                if (orderItemRepo == null)
                    orderItemRepo = new OrderItemRepo(context);
                return orderItemRepo;
            }
        }
        public PermissionRepo PermissionRepo
        {
            get
            {
                if (permissionRepo == null)
                    permissionRepo = new PermissionRepo(context);
                return permissionRepo;
            }
        }
        public RejectionReasonRepo RejectionReasonRepo
        {
            get
            {
                if (rejectionReasonRepo == null)
                    rejectionReasonRepo = new RejectionReasonRepo(context);
                return rejectionReasonRepo;
            }
        }
        public SafeRepo SafeRepo
        {
            get
            {
                if (safeRepo == null)
                    safeRepo = new SafeRepo(context);
                return safeRepo;
            }
        }

        public ShippingTypeRepo ShippingTypeRepo
        {
            get
            {
                if (shippingTypeRepo == null)
                    shippingTypeRepo = new ShippingTypeRepo(context);
                return shippingTypeRepo;
            }
        }
        public TraderProfileRepo TraderProfileRepo
        {
            get
            {
                if (traderProfileRepo == null)
                    traderProfileRepo = new TraderProfileRepo(context);
                return traderProfileRepo;
            }
        }


        public WeightRepo WeightRepo
        {
            get
            {
                if (weightRepo == null)
                    weightRepo = new WeightRepo(context);
                return weightRepo;
            }
        }
        public PermissionActionRepo PermissionActionRepo
        {
            get
            {
                if (permissionActionRepo == null)
                    permissionActionRepo = new PermissionActionRepo(context);
                return permissionActionRepo;
            }
        }
        public ActionPermissionRepo ActionPermissionRepo
        {
            get
            {
                if (actionPermissionRepo == null)
                    actionPermissionRepo = new ActionPermissionRepo(context);
                return actionPermissionRepo;
            }
        }
        public UserPermissionRepo UserPermissionRepo
        {
            get
            {
                if (userPermissionRepo == null)
                    userPermissionRepo = new UserPermissionRepo(context);
                return userPermissionRepo;
            }
        }
        public void save()
        {
            context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }




    }
}
