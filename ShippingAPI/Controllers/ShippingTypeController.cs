using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.Data;
using ShippingAPI.DTOS.ShippingTypeDTOs;
using ShippingAPI.Models;
using ShippingAPI.Repositories;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingTypeController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly UnitOfWork unitOfWork;

        public ShippingTypeController(UnitOfWork unit , IMapper mapper)
        {
            this.unitOfWork = unit;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllShippingTypes()
        {
            var shippingTypes = unitOfWork.ShippingTypeRepo.getAll();
            if(shippingTypes == null || !shippingTypes.Any())
            {
                return NotFound("No shipping types found.");
            }
            List<displayShippingTypeDTO> result = mapper.Map<List<displayShippingTypeDTO>>(shippingTypes);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetShippingTypeById(int id)
        {
            var shippingType = unitOfWork.ShippingTypeRepo.getById(id);
            if (shippingType == null)
            {
                return NotFound($"Shipping type with ID {id} not found.");
            }
            displayShippingTypeDTO result = mapper.Map<displayShippingTypeDTO>(shippingType);
            return Ok(result);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetShippingTypeByName(string name)
        {
            var shippingType = unitOfWork.ShippingTypeRepo.getByTypeName(name);
            if (shippingType == null)
            {
                return NotFound($"Shipping type with name '{name}' not found.");
            }
            List<displayShippingTypeDTO> result = mapper.Map<List<displayShippingTypeDTO>>(shippingType);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateShippingType(addShippingTypeDTO shippingTypeDto)
        {
            if (shippingTypeDto == null)
            {
                return BadRequest("Invalid shipping type data.");
            }
            var shippingType = mapper.Map<ShippingType>(shippingTypeDto);
            unitOfWork.ShippingTypeRepo.add(shippingType);
            unitOfWork.save();
            displayShippingTypeDTO result = mapper.Map<displayShippingTypeDTO>(shippingType);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateShippingType(int id, addShippingTypeDTO shippingTypeDto)
        {
            if (shippingTypeDto == null)
            {
                return BadRequest("Invalid shipping type data.");
            }
            var existingShippingType = unitOfWork.ShippingTypeRepo.getById(id);
            if (existingShippingType == null)
            {
                return NotFound($"Shipping type with ID {id} not found.");
            }
            mapper.Map(shippingTypeDto, existingShippingType);
            unitOfWork.ShippingTypeRepo.edit(existingShippingType);
            unitOfWork.save();
            displayShippingTypeDTO result = mapper.Map<displayShippingTypeDTO>(existingShippingType);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShippingType(int id)
        {
            var shippingType = unitOfWork.ShippingTypeRepo.getById(id);
            if (shippingType == null)
            {
                return NotFound($"Shipping type with ID {id} not found.");
            }
            displayShippingTypeDTO result = mapper.Map<displayShippingTypeDTO>(shippingType);
            unitOfWork.ShippingTypeRepo.delete(id);
            unitOfWork.save();
            return Ok(result);
        }
    }
}
