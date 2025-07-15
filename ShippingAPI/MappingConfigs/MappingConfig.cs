using AutoMapper;
using ShippingAPI.DTOS;
using ShippingAPI.DTOS.city_govern;
using ShippingAPI.DTOS.courier;
using ShippingAPI.DTOS.CustomPriceDTOs;
using ShippingAPI.DTOS.Employee;
using ShippingAPI.DTOS.ExtraVillagePriceDTOs;
using ShippingAPI.DTOS.FinancialTransferDtOs;
using ShippingAPI.DTOS.OrderDTOs;
using ShippingAPI.DTOS.OrderItemDTOs;
using ShippingAPI.DTOS.Permissions;
using ShippingAPI.DTOS.Register;
using ShippingAPI.DTOS.RegisterAndLogin;
using ShippingAPI.DTOS.RejectionReasonDTOs;
using ShippingAPI.DTOS.Reports;
using ShippingAPI.DTOS.Reports.OrderDelivery;
using ShippingAPI.DTOS.Saves;
using ShippingAPI.DTOS.ShippingTypeDTOs;
using ShippingAPI.DTOS.TraderDTOs;
using ShippingAPI.DTOS.WeightDTOs;
using ShippingAPI.Models;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShippingAPI.MappingConfigs
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CustomPrice, addCustomPriceDTO>().ReverseMap();
            CreateMap<CustomPrice, displayCustomPriceDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.TraderName = src.TraderProfile?.User?.FullName ?? "Unknown Trader";
                    dest.CityName = src.City?.Name ?? "Unknown City";
                    dest.TraderId = src.TraderId;
                    dest.CityId = src.CityId;
                }).ReverseMap();
            CreateMap<ShippingType, addShippingTypeDTO>().ReverseMap();
            CreateMap<ShippingType, displayShippingTypeDTO>().ReverseMap();
            CreateMap<Order, addOrderDTO>().ReverseMap();
            CreateMap<Order, displayOrderDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.BranchName = src.Branch?.Name ?? "";
                    dest.TraderName = src.TraderProfile?.User?.FullName ?? "";
                    dest.CourierName = src.CourierProfile?.User?.FullName ?? "";
                    dest.CityName = src.City?.Name ?? "";
                    dest.RejectionReason = src.RejectionReason?.Reason ?? "";
                    dest.GovernorateName = src.Governorate?.Name ?? "";
                    dest.ShippingTypeName = src.ShippingType?.TypeName ?? "";

                }
                ).ReverseMap();
            CreateMap<TraderProfile, TraderProfileDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.Email = src.User.Email;
                    dest.FullName = src.User.FullName;
                    dest.Address = src.User.Address;
                    dest.BranchName = src.Branch.Name;
                    dest.IsActive = src.User.IsActive;
                    dest.Phone = src.User.PhoneNumber;
                    dest.CityName = src.City.Name;
                    dest.GovernorateName = src.City.Governorate.Name;
                    dest.BranchName = src.Branch.Name;
                }
                ).ReverseMap();
            CreateMap<UpdateTraderDTO, TraderProfile>().AfterMap(
                (src, dest) =>
                {
                    dest.User.Email = src.Email;
                    dest.User.FullName = src.FullName;
                    dest.User.Address = src.Address;
                    dest.User.PhoneNumber = src.Phone;
                    dest.User.IsActive = src.IsActive;
                });

            CreateMap<Weight, addWeightDTO>().ReverseMap();
            CreateMap<Weight, displayWeightDTO>().ReverseMap();
            CreateMap<RejectionReason, addRejectionReasonDTO>().ReverseMap();
            CreateMap<RejectionReason, displayRejectionReasonDTO>().ReverseMap();
            CreateMap<OrderItem, addOrderItemDTO>().ReverseMap();
            CreateMap<OrderItem, displayOrderItemDTO>().ReverseMap();
            CreateMap<ExtraVillagePrice, addExtraPriceDTO>().ReverseMap();
            CreateMap<ExtraVillagePrice, displayExtraPriceDTO>().ReverseMap();
            CreateMap<Bank, BankDTO>()
                 .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.Name : string.Empty));
            CreateMap<BankDTO, Bank>()
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
                .ForMember(dest => dest.Branch, opt => opt.Ignore());
            CreateMap<Safe, SavesDto>()
                 .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.Name : string.Empty));

            CreateMap<SavesDto, Safe>()
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
                .ForMember(dest => dest.Branch, opt => opt.Ignore());

            CreateMap<ApplicationUser, UserProfileDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
            CreateMap<Permission, PermissionDto>().ReverseMap();
            CreateMap<ActionType, ActionTypeDto>().ReverseMap();
            CreateMap<PermissionAction, PermissionActionDto>().ReverseMap();
            CreateMap<PermissionAction,Permissionactioncreate >()
    .ForMember(dest => dest.PermissionName, opt => opt.MapFrom(src => src.Permission.Name))
    .ForMember(dest => dest.ActionTypeName, opt => opt.MapFrom(src => src.ActionType.Name));

            CreateMap<RegisterDTO, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
            CreateMap<RegisterEmployeeDTO, ApplicationUser>();


            CreateMap<FinancialTransferDto, FinancialTransfer>();

            CreateMap<FinancialTransfer, FinancialTransferViewDto>()
            .ForMember(dest => dest.SourceName, opt => opt.MapFrom(src => src.SourceBank != null ? src.SourceBank.Name : src.SourceSafe != null ? src.SourceSafe.Name : null))
            .ForMember(dest => dest.DestinationName, opt => opt.MapFrom(src => src.DestinationBank != null ? src.DestinationBank.Name :  src.DestinationSafe != null ? src.DestinationSafe.Name : null))
            .ForMember(dest => dest.AdminName, opt => opt.MapFrom(src =>  src.Admin != null ? src.Admin.UserName : null));

            CreateMap<FinancialTransfer, BankTransactionReportDto>()
           .ForMember(dest => dest.BankName, opt => opt.MapFrom(src => src.SourceBank != null ? src.SourceBank.Name : src.DestinationBank != null ? src.DestinationBank.Name : string.Empty))
           .ForMember(dest => dest.Credit, opt => opt.MapFrom(src => src.DestinationBankId != null ? src.Amount : 0))
           .ForMember(dest => dest.AdminName, opt => opt.MapFrom(src => src.Admin.FullName != null ? src.Admin.FullName : ""))
           .ForMember(dest => dest.Debit, opt => opt.MapFrom(src => src.SourceBankId != null ? src.Amount : 0));
           

            CreateMap<FinancialTransfer, SafeTransactionReportDto>()
            .ForMember(dest => dest.SafeName, opt => opt.MapFrom(src => src.SourceSafe != null ? src.SourceSafe.Name : src.DestinationSafe != null ? src.DestinationSafe.Name : string.Empty))
            .ForMember(dest => dest.Credit, opt => opt.MapFrom(src => src.DestinationSafeId != null ? src.Amount : 0))
            .ForMember(dest => dest.AdminName, opt => opt.MapFrom(src => src.Admin.FullName != null ? src.Admin.FullName : ""))
            .ForMember(dest => dest.Debit, opt => opt.MapFrom(src => src.SourceSafeId != null ? src.Amount : 0));
            CreateMap<City, cityDTO>().ReverseMap();
            //  .ForMember(dest => dest.GoverrateName, opt => opt.MapFrom(src => src.Governorate.Name))
            CreateMap<City, cityidDTO>()
                .ForMember(dest => dest.GoverrateName, opt => opt.MapFrom(src => src.Governorate.Name))
                .ReverseMap();

            CreateMap<cityDTO, City>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PricePerKg, opt => opt.MapFrom(src => src.PricePerKg))
                .ForMember(dest => dest.PickupCost, opt => opt.MapFrom(src => src.PickupCost))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.Governorate, opt => opt.Ignore()); 

            CreateMap<cityidDTO, City>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PricePerKg, opt => opt.MapFrom(src => src.PricePerKg))
                .ForMember(dest => dest.PickupCost, opt => opt.MapFrom(src => src.PickupCost))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.Governorate, opt => opt.Ignore()); 


            CreateMap<Governorate, governrateDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))

                .ReverseMap();
            CreateMap<Governorate, governrateidDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))

                .ReverseMap();
            CreateMap<Branch, BranchIDdto>()
     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
     .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));

            CreateMap<BranchIDdto, Branch>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.City, opt => opt.Ignore()) 
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId));

            CreateMap<branchDTO, Branch>().ReverseMap();

            CreateMap<CourierProfile, CreateCourierDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.User.PasswordHash))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.SelectedGovernorateIds, opt => opt.MapFrom(src => src.CourierGovernorates.Select(g => g.GovernorateId)))
                .ForMember(dest => dest.SelectedBranchIds, opt => opt.MapFrom(src => src.CourierBranches.Select(b => b.BranchId))).ReverseMap();

            CreateMap<CourierProfile, displaycourier>()
    .AfterMap((src, dest) =>
    {
        dest.UserName = src.User.UserName;
        dest.Email = src.User.Email;
        dest.Password = src.User.PasswordHash;
        dest.FullName = src.User.FullName;
        dest.Address = src.User.Address;
        dest.PhoneNumber = src.User.PhoneNumber;
        dest.SelectedGovernorates = src.CourierGovernorates?
            .Select(c => c.Governorate.Name)
            .Distinct()
            .ToList();

        dest.SelectedBranchs = src.CourierBranches?
            .Select(b => b.Branch.Name)
            .ToList();
        dest.SelectedBranchsId = src.CourierBranches?.Select(b => b.BranchId).ToList();
    })
    .ReverseMap();
            CreateMap<Order, OrderReportDto>()
     .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
     .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
     .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
     .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone1))
     .ForMember(dest => dest.TraderName, opt => opt.MapFrom(src => src.TraderProfile.User.FullName))
     .ForMember(dest => dest.CourierName, opt => opt.MapFrom(src => src.CourierProfile.User.FullName))
     .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
     .ForMember(dest => dest.GovernorrateName, opt => opt.MapFrom(src => src.Governorate.Name))
     .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
     .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.ToString()))
     .ForMember(dest => dest.OrderCost, opt => opt.MapFrom(src => src.OrderCost))
     .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.TotalCost))
     .ForMember(dest => dest.TotalWeight, opt => opt.MapFrom(src => src.TotalWeight))
     .ForMember(dest => dest.SerialNumber, opt => opt.Ignore());
     CreateMap<RegisterEmployeeDTO, ApplicationUser>()
    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
            CreateMap<UpdateEmployeeDTO, ApplicationUser>()
    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));
            CreateMap<OrderStatusHistory, OrderStatusLogDto>()
    .ForMember(dest => dest.OldStatus, opt => opt.MapFrom(src => src.OldStatus.ToString()))
    .ForMember(dest => dest.NewStatus, opt => opt.MapFrom(src => src.NewStatus.ToString()))
    .ForMember(dest => dest.ChangedBy, opt => opt.MapFrom(src => src.ChangedByUser.FullName));
        }


    }
}
