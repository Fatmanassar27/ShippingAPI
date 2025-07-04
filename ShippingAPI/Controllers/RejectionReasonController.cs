using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS.RejectionReasonDTOs;
using ShippingAPI.DTOS.WeightDTOs;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RejectionReasonController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;
        public RejectionReasonController(UnitOfWork unit , IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllRejectionReasons()
        {
            var rejectionReasons = unit.RejectionReasonRepo.getAll();
            if (rejectionReasons == null || !rejectionReasons.Any())
            {
                return NotFound("No rejection reasons found.");
            }
            List<displayRejectionReasonDTO> result = mapper.Map<List<displayRejectionReasonDTO>>(rejectionReasons);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetWeightById(int id)
        {
            var rejectionReason = unit.RejectionReasonRepo.getById(id);
            if (rejectionReason == null)
            {
                return NotFound($"RejectionReason with ID {id} not found.");
            }
            displayRejectionReasonDTO result = mapper.Map<displayRejectionReasonDTO>(rejectionReason);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult addeRejectionReason(addRejectionReasonDTO rejectionReasonDto)
        {
            if (rejectionReasonDto == null)
            {
                return BadRequest("RejectionReason data is null.");
            }
            var rejectionReason = mapper.Map<RejectionReason>(rejectionReasonDto);
            unit.RejectionReasonRepo.add(rejectionReason);
            unit.save();
            displayRejectionReasonDTO result = mapper.Map<displayRejectionReasonDTO>(rejectionReason);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult updateRejectionReason(int id, addRejectionReasonDTO rejectionReasonDto)
        {
            if (rejectionReasonDto == null)
            {
                return BadRequest("RejectionReason data is null.");
            }
            var existingRejectionReason = unit.RejectionReasonRepo.getById(id);
            if (existingRejectionReason == null)
            {
                return NotFound($"RejectionReason with ID {id} not found.");
            }
            var updatedRejectionReason = mapper.Map(rejectionReasonDto, existingRejectionReason);
            unit.RejectionReasonRepo.edit(updatedRejectionReason);
            unit.save();
            displayRejectionReasonDTO result = mapper.Map<displayRejectionReasonDTO>(updatedRejectionReason);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult deleteRejectionReason(int id)
        {
            var rejectionReason = unit.RejectionReasonRepo.getById(id);
            if (rejectionReason == null)
            {
                return NotFound($"RejectionReason with ID {id} not found.");
            }
            unit.RejectionReasonRepo.delete(id);
            unit.save();
            return Ok($"RejectionReason with ID {id} deleted successfully.");

        }

    }
}
