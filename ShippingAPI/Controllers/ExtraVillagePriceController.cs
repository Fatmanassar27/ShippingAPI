using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS.ExtraVillagePriceDTOs;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtraVillagePriceController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly IMapper mapper;

        public ExtraVillagePriceController(UnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult getAllُExtraPrices()
        {
            var prices = unit.ExtraVillagePriceRepo.getAll();
            var result = mapper.Map<List<displayExtraPriceDTO>>(prices);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult getExtraPriceById(int id)
        {
            var price = unit.ExtraVillagePriceRepo.getById(id);
            if (price == null)
                return NotFound($"Extra village price With Id= {id} not found.");
            return Ok(mapper.Map<displayExtraPriceDTO>(price));
        }

        [HttpPost]
        public IActionResult addExtraPrice(addExtraPriceDTO dto)
        {
            if (dto.IsActive)
            {
                var activeExists = unit.ExtraVillagePriceRepo.getAll().Any(p => p.IsActive);
                if (activeExists)
                    return BadRequest("Only one active extra village price is allowed.");
            }

            var newPrice = mapper.Map<ExtraVillagePrice>(dto);
            unit.ExtraVillagePriceRepo.add(newPrice);
            unit.save();

            return Ok(mapper.Map<displayExtraPriceDTO>(newPrice));
        }

        [HttpPut("{id}")]
        public IActionResult editExtraPrice(int id, addExtraPriceDTO dto)
        {
            var existing = unit.ExtraVillagePriceRepo.getById(id);
            if (existing == null)
                return NotFound($"Extra village price with Id = {id} not found.");

            if (dto.IsActive && !existing.IsActive)
            {
                var anotherActive = unit.ExtraVillagePriceRepo.getAll()
                    .Any(p => p.IsActive && p.Id != id);

                if (anotherActive)
                    return BadRequest("Only one active extra village price is allowed.");
            }

            mapper.Map(dto, existing);
            unit.ExtraVillagePriceRepo.edit(existing);
            unit.save();

            return Ok(mapper.Map<displayExtraPriceDTO>(existing));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var price = unit.ExtraVillagePriceRepo.getById(id);
            if (price == null)
                return NotFound("Extra village price not found.");

            unit.ExtraVillagePriceRepo.delete(id);
            unit.save();

            return Ok(mapper.Map<displayExtraPriceDTO>(price));
        }
    }
}
