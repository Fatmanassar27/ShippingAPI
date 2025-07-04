using AutoMapper;
using ShippingAPI.DTOS.CustomPriceDTOs;
using ShippingAPI.DTOS.OrderDTOs;
using ShippingAPI.DTOS.OrderItemDTOs;
using ShippingAPI.DTOS.Permissions;
using ShippingAPI.DTOS.Register;
using ShippingAPI.DTOS.RegisterAndLogin;
using ShippingAPI.DTOS.ShippingTypeDTOs;
using ShippingAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShippingAPI.MappingConfigs
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // CustomPrice Mapping
            CreateMap<CustomPrice,addCustomPriceDTO>().ReverseMap();
            CreateMap<CustomPrice, displayCustomPriceDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.UserName = src.TraderProfile?.User.UserName ?? "Unknown User";
                    dest.CityName = src.City?.Name ?? "Unknown City";
                }).ReverseMap();

            // ShippingType Mapping
            CreateMap<ShippingType, addShippingTypeDTO>().ReverseMap();
            CreateMap<ShippingType, displayShippingTypeDTO>().ReverseMap();

            // Order Mapping
            CreateMap<Order, addOrderDTO>().ReverseMap();
            CreateMap<Order , displayOrderDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.BranchName = src.Branch.Name;
                    dest.TraderName = src.TraderProfile.User.FullName;
                    dest.CourierName = src.CourierProfile.User.FullName;
                    dest.CityName = src.City.Name;
                    dest.RejectionReason = src.RejectionReason.Reason;
                    dest.GovernorateName = src.Governorate.Name;

                }
                ).ReverseMap();
            // OrderItem Mapping
            CreateMap<OrderItem, addOrderItemDTO>().ReverseMap();
            CreateMap<OrderItem, displayOrderItemDTO>().ReverseMap();
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
        }
    }
}
