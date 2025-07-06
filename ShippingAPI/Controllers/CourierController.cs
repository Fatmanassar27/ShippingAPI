using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS.courier;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;

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
        public async Task<IActionResult> Createcourier([FromBody] CreateCourierDTO courierdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existinguaser = await usermanger.FindByEmailAsync(courierdto.Email);
            if (existinguaser != null)
            {
                return BadRequest(new { message = "Email already exists" });
            }
            var allGovs = uow.GovernateRepo.getAll().Select(g => g.Id).ToList();
            var invalidGovs = courierdto.SelectedGovernorateIds.Except(allGovs).ToList();

            if (invalidGovs.Any())
            {
                return BadRequest(new
                {
                    message = "Invalid Governorate IDs.",
                    invalidGovernorateIds = invalidGovs
                });
            }

            // التحقق من الفروع (لو عايزة تضيفي كمان)
            var allBranches = uow.BranchRepo.getAll().Select(b => b.Id).ToList();
            var invalidBranches = courierdto.SelectedBranchIds.Except(allBranches).ToList();

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
                UserName = courierdto.UserName,
                Email = courierdto.Email,
                Address = courierdto.Address,
                PhoneNumber = courierdto.PhoneNumber,
                FullName = courierdto.FullName,

            };
            var result = await usermanger.CreateAsync(newcourier, courierdto.Password);
            await usermanger.UpdateAsync(newcourier);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Failed to create courier", errors = result.Errors });
            }
            if (!await roleManager.RoleExistsAsync("Courier"))
            {
                await roleManager.CreateAsync(new IdentityRole("Courier"));
            }

            await usermanger.AddToRoleAsync(newcourier, "Courier");

            var courierProfile = new CourierProfile
            {
                UserId = newcourier.Id,
                DiscountType = courierdto.DiscountType,
                OrderShare = courierdto.OrderShare,

                CourierGovernorates = courierdto.SelectedGovernorateIds.Select(govId => new CourierGovernorate
                {
                    GovernorateId = govId,
                    CourierId = newcourier.Id
                }).ToList(),

                CourierBranches = courierdto.SelectedBranchIds.Select(branchId => new CourierBranch
                {
                    BranchId = branchId,
                    CourierId = newcourier.Id
                }).ToList()
            };
            uow.CourierProfileRepo.add(courierProfile);
            uow.save();

            return Ok("Courier created successfully.");
        }


        [HttpGet("getcouriers")]
        public IActionResult GetCouriers()
        {
            var couriers = uow.CourierProfileRepo.getAllWithUser();
            if (couriers == null || !couriers.Any())
            {
                return NotFound("There Are No Couriers!");
            }
            var courierDtos = mapper.Map<List<CreateCourierDTO>>(couriers); 
            return Ok(courierDtos);
            //return Ok(couriers);
        }

        [HttpGet("getcourierbyid/{id}")]
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
            return Ok("Courier updated successfully.");

        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCourier(int id)
        {
            var courier = uow.CourierProfileRepo.getById(id);
            if (courier == null)
            {
                return NotFound("The Courier Is Not Found");
            }
            uow.CourierProfileRepo.delete(id);
            uow.save();
            return Ok("Courier deleted successfully.");
        }
    }
}
    


