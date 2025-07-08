using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS.TraderDTOs;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraderController : ControllerBase
    {
        UnitOfWork unit;
        private readonly IMapper mapper;
        UserManager<ApplicationUser> userManager;

        public TraderController(UnitOfWork unit , UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.unit = unit;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpPost("RegisterTrader")]
        public async Task<IActionResult> RegisterTrader(RegisterTraderDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return BadRequest("This Email already exists.");

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName,
                Address = dto.Address,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var traderProfile = new TraderProfile
            {
                UserId = user.Id,
                StoreName = dto.StoreName,
                GovernorateId = dto.GovernorateId,
                CityId = dto.CityId,
                BranchId = dto.BranchId,
                CustomPickupCost = dto.CustomPickupCost ?? 0,
                RejectedOrderShippingShare = dto.RejectedOrderShippingShare
            };

            unit.TraderProfileRepo.add(traderProfile);
            unit.save();

            return Ok("Trader Registered Successfully!" );
        }

        [HttpGet]
        public IActionResult getAll()
        {
            var traders = unit.TraderProfileRepo.getAllWithUser();
            var result = mapper.Map<List<TraderProfileDTO>>(traders);
            return Ok(result);
        }

        [HttpGet("getById/{id}")]
        public IActionResult getById(string id)
        {
            var trader = unit.TraderProfileRepo.getByIdWithUser(id);
            if (trader == null)
                return NotFound("Trader not found." );

            var result = mapper.Map<TraderProfileDTO>(trader);
            return Ok(result);
        }

        [HttpGet("getByName")]
        public IActionResult getByName(string name)
        {
            var trader = unit.TraderProfileRepo.getByStoreName(name);
            if (trader == null)
                return NotFound("Trader not found by store name." );

            var result = mapper.Map<TraderProfileDTO>(trader);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult editTrader(string id, UpdateTraderDTO dto)
        {
            var trader = unit.TraderProfileRepo.getByIdWithUser(id);
            if (trader == null)
                return NotFound("Trader not found." );

            mapper.Map(dto, trader);
            unit.TraderProfileRepo.edit(trader);
            unit.save();

            return Ok("Trader updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var trader = unit.TraderProfileRepo.getByIdWithUser(id);
            if (trader == null)
                return NotFound(new {Message = "Trader not found." });

            var user = await userManager.FindByIdAsync(id);
            if (user != null)
                await userManager.DeleteAsync(user);

            unit.TraderProfileRepo.delete(id);
            unit.save();

            return Ok("Trader deleted successfully.");
        }




    }
}
