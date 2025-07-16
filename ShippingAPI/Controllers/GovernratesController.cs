using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS.city_govern;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin,Trader")]
    public class GovernratesController : ControllerBase
    {
        private readonly UnitOfWork uow;
        private readonly IMapper map;

        public GovernratesController(UnitOfWork uow, IMapper map)
        {
            this.uow = uow;
            this.map = map;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Trader,Courier")]
        public IActionResult GetGovernrates()
        {
            var governrates = uow.GovernateRepo.getAll();
            if (governrates == null || !governrates.Any())
            {
                return NotFound("There Are No Governrates!");
            }
            var newGovernrates = map.Map<List<governrateidDTO>>(governrates);
            return Ok(newGovernrates);
        }
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Trader,Courier")]
        public IActionResult GetGovernrateById(int id)
        {
            var governrate = uow.GovernateRepo.getById(id);
            if (governrate == null)
            {
                return NotFound("The Governrate Is Not Found");
            }
            var governrateDto = map.Map<governrateidDTO>(governrate);
            return Ok(governrateDto);
        }
        [HttpGet("getgovernratebyname/{name}")]
        [Authorize(Roles = "Admin,Trader,Courier")]
        public IActionResult GetGovernrateByName(string name)
        {
            var governrate = uow.GovernateRepo.getByName(name);
            if (governrate == null)
            {
                return NotFound("The Governrate Is Not Found");
            }
            var governrateDto = map.Map<governrateidDTO>(governrate);
            return Ok(governrateDto);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddGovernrate(governrateDTO governrateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingGovernrate = uow.GovernateRepo.getByName(governrateDto.Name);
            if (existingGovernrate != null)
            {
                return BadRequest("The Governrate Already Exists");
            }
            var governrate = map.Map<Governorate>(governrateDto);
            uow.GovernateRepo.add(governrate);
            uow.save();
            return CreatedAtAction(nameof(GetGovernrateById), new { id = governrate.Id }, governrateDto);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateGovernrate( governrateidDTO governrateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingGovernrate = uow.GovernateRepo.getById(governrateDto.Id);
            if (existingGovernrate == null)
            {
                return NotFound(new { message = "The Governrate Is Not Found" });
            }
            var existingByName = uow.GovernateRepo.getByName(governrateDto.Name);
            if (existingByName != null && existingByName.Id != governrateDto.Id)
            {
                return BadRequest(new {message= "The Governrate Name Already Exists" });
            }
            map.Map(governrateDto, existingGovernrate);
            uow.GovernateRepo.edit(existingGovernrate);
            uow.save();
            return Ok(new {message= "Governrate Updated Successfully" });

        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteGovernrate(int id)
        {
            try
            {
                var governrate = uow.GovernateRepo.getById(id);
                if (governrate == null)
                {
                    return NotFound("The governorate was not found.");
                }

                // Check if the governorate is linked to any couriers
                var hasCouriers = uow.CourierGovernorateRepo.getAll().Any(c => c.GovernorateId == id);
                if (hasCouriers)
                {
                    return BadRequest("Cannot delete this governorate because it is linked to one or more couriers.");
                }

                // Proceed with deletion if no relations exist
                uow.GovernateRepo.delete(id);
                uow.save();
                return Ok(new { message = "Governorate deleted successfully." });
            }
            catch (Exception)
            {
                return BadRequest("Unable to delete this governorate because it has related data.");
            }
        }



        [HttpGet("names")]
        //[Authorize(Roles = "Admin,Trader,Courier")]
        public IActionResult getallgovernnames()
        {
            List<string> names = uow.GovernateRepo.getAll().Select(b => b.Name).ToList();
            return Ok(names);
        }

    }

}
