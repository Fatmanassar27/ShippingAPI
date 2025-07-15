using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShippingAPI.DTOS.courier;
using ShippingAPI.DTOS.RejectionReasonDTOs;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;
using System.Security.Claims;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourierController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> usermanger;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UnitOfWork uow;
        private readonly IMapper mapper;

        public CourierController(UserManager<ApplicationUser> usermanger, RoleManager<IdentityRole> roleManager, UnitOfWork uow, IMapper mapper)
        {
            this.usermanger = usermanger;
            this.roleManager = roleManager;
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Createcourier([FromBody] CreateCourierDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(v => v.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .FirstOrDefault();
                return BadRequest(new { message = error ?? "Invalid input" });
            }
            var existinguaser = await usermanger.FindByEmailAsync(dto.Email);
            if (existinguaser != null)
            {
                return BadRequest(new { message = "Email already exists" });
            }
            var allGovs = uow.GovernateRepo.getAll().Select(g => g.Id).ToList();
            var invalidGovs = dto.SelectedGovernorateIds.Except(allGovs).ToList();

            if (invalidGovs.Any())
            {
                return BadRequest(new
                {
                    message = "Invalid Governorate IDs.",
                    invalidGovernorateIds = invalidGovs
                });
            }
            var allBranches = uow.BranchRepo.getAll().Select(b => b.Id).ToList();
            var invalidBranches = dto.SelectedBranchIds.Except(allBranches).ToList();

            if (invalidBranches.Any())
            {
                return BadRequest(new
                {
                    message = "Invalid Branch IDs.",
                    invalidBranchIds = invalidBranches
                });
            }
            var newcourier = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                FullName = dto.FullName,

            };
            var result = await usermanger.CreateAsync(newcourier, dto.Password);
            await usermanger.UpdateAsync(newcourier);
            if (!result.Succeeded)
            {
                var firstError = result.Errors.FirstOrDefault()?.Description ?? "Creation failed";
                return BadRequest(new { message = firstError });
            }
            if (!await roleManager.RoleExistsAsync("Courier"))
            {
                await roleManager.CreateAsync(new IdentityRole("Courier"));
            }

            await usermanger.AddToRoleAsync(newcourier, "Courier");

            var courierProfile = new CourierProfile
            {
                UserId = newcourier.Id,
                DiscountType = dto.DiscountType,
                OrderShare = dto.OrderShare,

                CourierGovernorates = dto.SelectedGovernorateIds.Select(govId => new CourierGovernorate
                {
                    GovernorateId = govId,
                    CourierId = newcourier.Id
                }).ToList(),

                CourierBranches =  dto.SelectedBranchIds.Select(branchId => new CourierBranch
                {
                    BranchId = branchId,
                    CourierId = newcourier.Id
                }).ToList()
            };
            uow.CourierProfileRepo.add(courierProfile);
            uow.save();

            return Ok(new {message= "Courier created successfully." });
        }


        [HttpGet("getcouriers")]
        [Authorize(Roles = "Admin,Trader")]
        public IActionResult GetCouriers()
        {
            var couriers = uow.CourierProfileRepo.getAllWithUser();
            if (couriers == null || !couriers.Any())
            {
                return NotFound("There Are No Couriers!");
            }
            var courierDtos = mapper.Map<List<displaycourier>>(couriers); 
            return Ok(courierDtos);
            //return Ok(couriers);
        }

        [HttpGet("getcourierbyid/{id}")]
        [Authorize(Roles = "Admin,Trader,Courier")]
        public IActionResult GetCourierById(string id)
        {
            var courier = uow.CourierProfileRepo.getByIdWithUser(id);
            if (courier == null)
            {
                return NotFound("The Courier Is Not Found");
            }
            var courierDtos = mapper.Map<CreateCourierDTO>(courier);
            return Ok(courierDtos);
            //return Ok(courier);
        }
        [HttpGet("getcourierbyname/{name}")]
        [Authorize(Roles = "Admin,Trader")]
        public IActionResult GetCourierByName(string name)
        {
            var courier = uow.CourierProfileRepo.getcourierbyname(name);
            if (courier == null)
            {
                return NotFound("The Courier Is Not Found");
            }
            //return Ok(courier);
            var courierDto = mapper.Map<CreateCourierDTO>(courier); 
            return Ok(courierDto);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCourier([FromBody] CreateCourierDTO courierdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingCourier = uow.CourierProfileRepo.getbyemail(courierdto.Email);
            if (existingCourier == null)
            {
                return NotFound("The Courier Is Not Found");
            }
            // Update the courier's properties
            existingCourier.User.Email = courierdto.Email;
            existingCourier.User.Address = courierdto.Address;
            existingCourier.User.PhoneNumber = courierdto.PhoneNumber;
            existingCourier.User.FullName = courierdto.FullName;
            existingCourier.DiscountType = courierdto.DiscountType;
            existingCourier.OrderShare = courierdto.OrderShare;
            // Update Governorates and Branches
            existingCourier.CourierGovernorates.Clear();
            existingCourier.CourierGovernorates = courierdto.SelectedGovernorateIds.Select(govId => new CourierGovernorate
            {
                GovernorateId = govId,
                CourierId = existingCourier.UserId
            }).ToList();
            existingCourier.CourierBranches.Clear();
            existingCourier.CourierBranches = courierdto.SelectedBranchIds.Select(branchId => new CourierBranch
            {
                BranchId = branchId,
                CourierId = existingCourier.UserId
            }).ToList();
            uow.CourierProfileRepo.edit(existingCourier);
            uow.save();
            return Ok(new { message = "Courier updated successfully." });

        }
        [HttpDelete("delete/{courierId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourier(string courierId)
        {
            var courierProfile = uow.CourierProfileRepo.getByIdWithUser(courierId);
            if (courierProfile == null)
                return NotFound(new { message = "Courier not found" });

            var hasBranches = uow.CourierProfileRepo.hasRelatedBranches(courierId);
            var hasGovernorates = uow.CourierProfileRepo.hasRelatedGovernorates(courierId);

            if (hasBranches || hasGovernorates)
            {
                return BadRequest(new
                {
                    message = "Cannot delete courier because there are related branches or governorates."
                });
            }

            uow.CourierProfileRepo.delete(courierId);

            var user = await usermanger.FindByIdAsync(courierId);
            if (user != null)
            {
          
                var roles = await usermanger.GetRolesAsync(user);
                if (roles.Any())
                {
                    var removeRolesResult = await usermanger.RemoveFromRolesAsync(user, roles);
                    if (!removeRolesResult.Succeeded)
                    {
                        return BadRequest(new { message = "Failed to remove user roles", errors = removeRolesResult.Errors });
                    }
                }
                var result = await usermanger.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest(new { message = "Failed to delete user", errors = result.Errors });
                }
            }

            uow.save();

            return Ok(new { message = "Courier deleted successfully" });
        }




        [HttpGet("courier-orders-display/{courierId}")]
        [Authorize(Roles = "Courier")]
        public IActionResult GetOrdersForCourierDisplay(string courierId)
        {
            var orders = uow.CourierProfileRepo.GetOrdersByCourierId(courierId);

            if (orders == null || !orders.Any())
                return NotFound(new { message = "No orders found for this courier." });
            var filteredOrders = orders
                .Where(order => order.Status != OrderStatus.RejectedWithPayment
                             && order.Status != OrderStatus.RejectedWithPartialPayment
                             && order.Status != OrderStatus.RejectedWithoutPayment)
                .ToList();

            var result = filteredOrders.Select(order => new OrderDisplayDTO
            {
                OrderId = order.Id,
                Status = (int)order.Status,
                MerchantName = order.TraderProfile?.User.FullName ?? "غير معروف",
                CustomerName = order.CustomerName,
                PhoneNumber = order.Phone1,
                Governorate = order.Governorate?.Name,
                city = order.City?.Name,
                branch = order.Branch?.Name,
                OrderCost = order.OrderCost,
            }).ToList();

            return Ok(result);
        }





        [HttpPut("update-order-status")]
        public IActionResult UpdateOrderStatus([FromBody] UpdateOrderStatusDTO dto)
        {
            var order = uow.OrderRepo.getById(dto.OrderId);

            if (order == null)
                return NotFound(new { message = "Order not found." });

            var oldStatus = order.Status;
            var newStatus = (OrderStatus)dto.NewStatus;
            if (oldStatus == newStatus)
            {
                return BadRequest(new { message = "The status is already the same." });
            }
            order.Status = newStatus;
            if (dto.RejectionReasonId != null)
            {
                order.RejectionReasonId = dto.RejectionReasonId;
            }

            uow.OrderRepo.edit(order);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var history = new OrderStatusHistory
            {
                OrderId = order.Id,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                ChangedByUserId = userId,
                Notes = $"Status updated by courier {order.Status.ToString()}",
                ChangedAt = DateTime.UtcNow
            };

            uow.context.OrderStatusHistories.Add(history);

            uow.save();

            return Ok(new { message = "Order status updated successfully." });
        }




        // الطلبات المرفوضة فقط الخاصة بكوريير معين
        [HttpGet("rejected-orders/{courierId}")]
        public IActionResult GetRejectedOrdersByCourierId(string courierId)
        {
            var rejectedOrders = uow.CourierProfileRepo.GetRejectedOrdersByCourierId(courierId);

            if (rejectedOrders == null || !rejectedOrders.Any())
            {
                return NotFound(new { message = "No rejected orders found for this courier." });
            }

            var result = rejectedOrders.Select(order => new RejectedOrderDisplayDTO
            {
                OrderId = order.Id,
                Status = (int)order.Status,
                MerchantName = order.TraderProfile?.User.FullName ?? "غير معروف",
                CustomerName = order.CustomerName,
                PhoneNumber = order.Phone1,
                Governorate = order.Governorate?.Name,
                City = order.City?.Name,
                Branch = order.Branch?.Name,
                OrderCost = order.OrderCost,
                RejectionReason = order.RejectionReason?.Reason ?? "غير محدد"
            }).ToList();

            return Ok(result);
        }


        // جميع أسباب الرفض المتاحة
        [HttpGet("rejection-reasons")]
        public IActionResult GetRejectionReasons()
        {
            var reasons = uow.CourierProfileRepo.GetRejectionReasons();

            if (reasons == null || !reasons.Any())
            {
                return NotFound(new { message = "No rejection reasons found." });
            }

            var mappedReasons = reasons.Select(r => new displayRejectionReasonDTO
            {
                Id = r.Id,
                Reason = r.Reason
            }).ToList();

            return Ok(mappedReasons);
        }





    }
}
    


