using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS.CustomPriceDTOs;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomPriceController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;
        public CustomPriceController(UnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult getAllCustomPrices() {
            var customPrices = unit.CustomPriceRepo.getAllWithObjs();
            if (customPrices == null || !customPrices.Any())
            {
                return NotFound("No custom prices found.");
            }
           
            List<displayCustomPriceDTO> result = mapper.Map<List<displayCustomPriceDTO>>(customPrices);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult getCustomPriceById(int id)
        {
            var customPrice = unit.CustomPriceRepo.getByIdWithObjs(id);
            if (customPrice == null)
            {
                return NotFound($"Custom price with ID {id} not found.");
            }

            displayCustomPriceDTO result = mapper.Map<displayCustomPriceDTO>(customPrice);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult addCustomPrice(addCustomPriceDTO customPriceDto)
        {
            if (customPriceDto == null)
            {
                return BadRequest("Invalid custom price data.");
            }

            var regionExists = unit.CityRepo.getById(customPriceDto.CityId) ;
            if (regionExists == null)
            {
                return BadRequest($"Region with ID {customPriceDto.CityId} does not exist.");
            }

            var customPrice = mapper.Map<CustomPrice>(customPriceDto);
            unit.CustomPriceRepo.add(customPrice);
            unit.save();

            displayCustomPriceDTO result = mapper.Map<displayCustomPriceDTO>(customPrice);
            return Ok(result);
        }



        [HttpPut("{id}")]
        public IActionResult updateCustomPrice(int id, addCustomPriceDTO customPriceDto)
        {
            if (customPriceDto == null || id <= 0)
            {
                return BadRequest("Invalid custom price data.");
            }
            var existingCustomPrice = unit.CustomPriceRepo.getById(id);
            if (existingCustomPrice == null)
            {
                return NotFound($"Custom price with ID {id} not found.");
            }
            var updatedCustomPrice = mapper.Map(customPriceDto, existingCustomPrice);
            unit.CustomPriceRepo.edit(updatedCustomPrice);
            unit.save();
            displayCustomPriceDTO result = mapper.Map<displayCustomPriceDTO>(updatedCustomPrice);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult deleteCustomPrice(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid custom price ID.");
            }
            var customPrice = unit.CustomPriceRepo.getById(id);
            if (customPrice == null)
            {
                return NotFound($"Custom price with ID {id} not found.");
            }
            displayCustomPriceDTO result = mapper.Map<displayCustomPriceDTO>(customPrice);
            unit.CustomPriceRepo.delete(id);
            unit.save();
            return Ok(result);
        }
    }
}
