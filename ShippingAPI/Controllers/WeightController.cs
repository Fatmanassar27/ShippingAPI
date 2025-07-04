using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS.WeightDTOs;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;
        public WeightController(UnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllWeights()
        {
            var weights = unit.WeightRepo.getAll();
            if (weights == null || !weights.Any())
            {
                return NotFound("No weights found.");
            }
            List<displayWeightDTO> result = mapper.Map<List<displayWeightDTO>>(weights);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult getWeightById(int id)
        {
            var weight = unit.WeightRepo.getById(id);
            if (weight == null)
            {
                return NotFound($"Weight with ID {id} not found.");
            }
            displayWeightDTO result = mapper.Map<displayWeightDTO>(weight);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult addeWeight(addWeightDTO weightDto)
        {
            if (weightDto == null)
            {
                return BadRequest("Weight data is null.");
            }
            var weight = mapper.Map<Weight>(weightDto);
            unit.WeightRepo.add(weight);
            unit.save();
            displayWeightDTO result = mapper.Map<displayWeightDTO>(weight);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult updateWeight(int id, addWeightDTO weightDto)
        {
            if (weightDto == null)
            {
                return BadRequest("Weight data is null.");
            }
            var existingWeight = unit.WeightRepo.getById(id);
            if (existingWeight == null)
            {
                return NotFound($"Weight with ID {id} not found.");
            }
            var updatedWeight = mapper.Map(weightDto, existingWeight);
            unit.WeightRepo.edit(updatedWeight);
            unit.save();
            displayWeightDTO result = mapper.Map<displayWeightDTO>(updatedWeight);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult deleteWeight(int id)
        {
            var weight = unit.WeightRepo.getById(id);
            if (weight == null)
            {
                return NotFound($"Weight with ID {id} not found.");
            }
            unit.WeightRepo.delete(id);
            unit.save();
            return Ok($"Weight with ID {id} deleted successfully.");

        }
    }
}
