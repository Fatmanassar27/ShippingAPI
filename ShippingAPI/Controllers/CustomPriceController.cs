using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,Trader,Courier")]
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
        [Authorize(Roles = "Admin,Trader,Courier")]
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

        [HttpGet("byTraderId/{traderId}")]
        [Authorize(Roles = "Admin,Trader,Courier")]
        public IActionResult getCustomPricesByTraderId(string traderId)
        {
            if (string.IsNullOrEmpty(traderId))
            {
                return BadRequest("Trader ID cannot be null or empty.");
            }
            var customPrices = unit.CustomPriceRepo.getByTraderId(traderId);
            if (customPrices == null || !customPrices.Any())
            {
                return NotFound($"No custom prices found for trader with ID {traderId}.");
            }
            List<displayCustomPriceDTO> result = mapper.Map<List<displayCustomPriceDTO>>(customPrices);
            return Ok(result);
        }

        [HttpGet("byCityId/{cityId}")]
        [Authorize(Roles = "Admin,Trader,Courier")]
        public IActionResult getCustomPricesByCityId(int cityId)
        {
            if (cityId <= 0)
            {
                return BadRequest("Invalid city ID.");
            }
            var customPrices = unit.CustomPriceRepo.getByCityId(cityId);
            if (customPrices == null || !customPrices.Any())
            {
                return NotFound($"No custom prices found for city with ID {cityId}.");
            }
            List<displayCustomPriceDTO> result = mapper.Map<List<displayCustomPriceDTO>>(customPrices);
            return Ok(result);
        }

        [HttpGet("byCityName/{cityName}")]
        [Authorize(Roles = "Admin,Trader,Courier")]
        public IActionResult getCustomPricesByCityName(string cityName)
        {
            if (string.IsNullOrEmpty(cityName))
            {
                return BadRequest("City name cannot be null or empty.");
            }
            var customPrices = unit.CustomPriceRepo.getByCityName(cityName);
            if (customPrices == null || !customPrices.Any())
            {
                return NotFound($"No custom prices found for city with name {cityName}.");
            }
            List<displayCustomPriceDTO> result = mapper.Map<List<displayCustomPriceDTO>>(customPrices);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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

            displayCustomPriceDTO result = mapper.Map<displayCustomPriceDTO>
                (unit.CustomPriceRepo.getByIdWithObjs(customPrice.Id));
            return Ok(result);
        }

        [HttpPost("Bulk")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddBulkCustomPrices(List<addCustomPriceDTO> customPriceDtos)
        {
            if (customPriceDtos == null || customPriceDtos.Count == 0)
                return BadRequest("No custom prices to save.");

            var customPrices = new List<CustomPrice>();

            foreach (var dto in customPriceDtos)
            {
                var city = unit.CityRepo.getById(dto.CityId);
                if (city == null)
                    return BadRequest($"City with ID {dto.CityId} does not exist.");

                var entity = mapper.Map<CustomPrice>(dto);
                customPrices.Add(entity);
            }

            unit.CustomPriceRepo.addRange(customPrices);
            unit.save();

            return Ok(new {message="Custom prices saved successfully." });
        }



        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
