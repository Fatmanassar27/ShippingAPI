using AutoMapper;
using ShippingAPI.DTOS;
using ShippingAPI.DTOS.city_govern;
using ShippingAPI.DTOS.courier;
using ShippingAPI.DTOS.CustomPriceDTOs;
using ShippingAPI.DTOS.ExtraVillagePriceDTOs;
using ShippingAPI.DTOS.FinancialTransferDtOs;
using ShippingAPI.DTOS.OrderDTOs;
using ShippingAPI.DTOS.OrderItemDTOs;
using ShippingAPI.DTOS.Permissions;
using ShippingAPI.DTOS.Register;
using ShippingAPI.DTOS.RegisterAndLogin;
using ShippingAPI.DTOS.RejectionReasonDTOs;
using ShippingAPI.DTOS.Saves;
using ShippingAPI.DTOS.ShippingTypeDTOs;
using ShippingAPI.DTOS.TraderDTOs;
using ShippingAPI.DTOS.WeightDTOs;
using ShippingAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShippingAPI.MappingConfigs
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // CustomPrice Mapping
            CreateMap<CustomPrice, addCustomPriceDTO>().ReverseMap();
            CreateMap<CustomPrice, addCustomPriceDTO>().ReverseMap();
            CreateMap<CustomPrice, displayCustomPriceDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.TraderName = src.TraderProfile?.User?.FullName ?? "Unknown Trader";
                    dest.CityName = src.City?.Name ?? "Unknown City";
                }).ReverseMap();

            // ShippingType Mapping
            CreateMap<ShippingType, addShippingTypeDTO>().ReverseMap();
            CreateMap<ShippingType, displayShippingTypeDTO>().ReverseMap();

            // Order Mapping
            CreateMap<Order, addOrderDTO>().ReverseMap();
            CreateMap<Order, displayOrderDTO>().AfterMap(
            CreateMap<Order, displayOrderDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.BranchName = src.Branch?.Name ?? "";
                    dest.TraderName = src.TraderProfile?.User?.FullName ?? "";
                    dest.CourierName = src.CourierProfile?.User?.FullName ?? "";
                    dest.CityName = src.City?.Name ?? "";
                    dest.RejectionReason = src.RejectionReason?.Reason ?? "";
                    dest.GovernorateName = src.Governorate?.Name ?? "";

                }
                ).ReverseMap();

            //Trader Mapping
            CreateMap<TraderProfile, TraderProfileDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.Email = src.User.Email;
                    dest.FullName = src.User.FullName;
                    dest.Address = src.User.Address;
                }
                ).ReverseMap();
            CreateMap<TraderProfile, UpdateTraderDTO>().ReverseMap();

            //Weight Mapping
            CreateMap<Weight, addWeightDTO>().ReverseMap();
            CreateMap<Weight, displayWeightDTO>().ReverseMap();

            //RejectionReason Mapping
            CreateMap<RejectionReason, addRejectionReasonDTO>().ReverseMap();
            CreateMap<RejectionReason, displayRejectionReasonDTO>().ReverseMap();

            // OrderItem Mapping
            CreateMap<OrderItem, addOrderItemDTO>().ReverseMap();
            CreateMap<OrderItem, displayOrderItemDTO>().ReverseMap();

            //ExtraVillagePrice Mapping
            CreateMap<ExtraVillagePrice, addExtraPriceDTO>().ReverseMap();
            CreateMap<ExtraVillagePrice, displayExtraPriceDTO>().ReverseMap();

            // Bank Mapping
            CreateMap<Bank, BankDTO>()
                 .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.Name : string.Empty));

            CreateMap<BankDTO, Bank>()
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
                .ForMember(dest => dest.Branch, opt => opt.Ignore());

            // Safe Mapping
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
            CreateMap<RegisterDTO, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            // FinancialTransfer Mapping
            CreateMap<FinancialTransferDto, FinancialTransfer>();

            CreateMap<FinancialTransfer, FinancialTransferViewDto>()
            .ForMember(dest => dest.SourceName, opt => opt.MapFrom(src => src.SourceBank != null ? src.SourceBank.Name : src.SourceSafe != null ? src.SourceSafe.Name : null))
            .ForMember(dest => dest.DestinationName, opt => opt.MapFrom(src => src.DestinationBank != null ? src.DestinationBank.Name :  src.DestinationSafe != null ? src.DestinationSafe.Name : null))
            .ForMember(dest => dest.AdminName, opt => opt.MapFrom(src =>  src.Admin != null ? src.Admin.UserName : null));

            CreateMap<FinancialTransfer, BankTransactionReportDto>()
           .ForMember(dest => dest.BankName, opt => opt.MapFrom(src => src.SourceBank != null ? src.SourceBank.Name : src.DestinationBank != null ? src.DestinationBank.Name : string.Empty))
           .ForMember(dest => dest.Credit, opt => opt.MapFrom(src => src.DestinationBankId != null ? src.Amount : 0))
           .ForMember(dest => dest.Debit, opt => opt.MapFrom(src => src.SourceBankId != null ? src.Amount : 0));

            CreateMap<FinancialTransfer, SafeTransactionReportDto>()
                .ForMember(dest => dest.SafeName, opt => opt.MapFrom(src => src.SourceSafe != null ? src.SourceSafe.Name : src.DestinationSafe != null ? src.DestinationSafe.Name : string.Empty))
                .ForMember(dest => dest.Credit, opt => opt.MapFrom(src => src.DestinationSafeId != null ? src.Amount : 0))
                .ForMember(dest => dest.Debit, opt => opt.MapFrom(src => src.SourceSafeId != null ? src.Amount : 0));

        }


    }
}
