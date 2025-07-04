using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS;
using ShippingAPI.DTOS.Saves;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavesController : ControllerBase
    {
        private readonly UnitOfWork _unit;
        private readonly IMapper _mapper;
        public SavesController(IMapper mapper, UnitOfWork unit)
        {
            _unit = unit;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllSaves()
        {
            var saves = _unit.SafeRepo.getAll();
            if (saves == null || !saves.Any())
            {
                return NotFound("No Saves found.");
            }
            var result = _mapper.Map<List<SavesDto>>(saves);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddSafe([FromBody] SavesDto safeDTO)
        {
            if (safeDTO == null)
            {
                return BadRequest("Invalid saves data.");
            }
            var safe = _mapper.Map<Safe>(safeDTO);
            _unit.SafeRepo.add(safe);
            _unit.save();
            return CreatedAtAction(nameof(GetAllSaves), new { id = safe.Id }, safe);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateSafe(int id, [FromBody] SavesDto safeDTO)
        {
            if (safeDTO == null || id != safeDTO.Id)
            {
                return BadRequest("Invalid safe data.");
            }
            var existingSafe = _unit.SafeRepo.getById(id);
            if (existingSafe == null)
            {
                return NotFound($"Safe with ID {id} not found.");
            }
            existingSafe.Name = safeDTO.Name;
            existingSafe.BranchId = safeDTO.BranchId;
            existingSafe.Balance = safeDTO.Balance;
            existingSafe.IsActive = safeDTO.IsActive;
            existingSafe.CreatedDate = safeDTO.CreatedDate;
            _unit.SafeRepo.edit(existingSafe);
            _unit.save();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSafe(int id)
        {
            var existingSafe = _unit.SafeRepo.getById(id);
            if (existingSafe == null)
            {
                return NotFound($"Safe with ID {id} not found.");
            }
            _unit.SafeRepo.delete(id);
            _unit.save();
            return NoContent();
        }
        [HttpGet("{id}")]
        public IActionResult GetSafeById(int id)
        {
            var existingSafe = _unit.SafeRepo.getById(id);
            if (existingSafe == null)
            {
                return NotFound($"Safe with ID {id} not found.");
            }
            var SafeDTO = _mapper.Map<SavesDto>(existingSafe);
            return Ok(SafeDTO);
        }
        [HttpPost("{id}/active")]
        public IActionResult ActivatedSafe(int id)
        {
            var existingSafe = _unit.SafeRepo.getById(id);
            if (existingSafe == null)
            {
                return NotFound($"Safe with ID {id} not found.");
            }
            if (existingSafe.IsActive)
            {
                return Ok("Safe is already active.");
            }
            existingSafe.IsActive = true;
            _unit.SafeRepo.edit(existingSafe);
            _unit.save();
            return Ok("Safe activated successfully.");
        }
        [HttpPost("{id}/disactive")]
        public IActionResult DisActivatedSafe(int id)
        {
            var existingSafe = _unit.SafeRepo.getById(id);
            if (existingSafe == null)
            {
                return NotFound($"Safe with ID {id} not found.");
            }
            if (!existingSafe.IsActive)
            {
                return Ok("Safe is already disactive.");
            }
            existingSafe.IsActive = false;
            _unit.SafeRepo.edit(existingSafe);
            _unit.save();
            return Ok("Safe disactivated successfully.");
        }

        [HttpGet("search")]
        public IActionResult SearchSafes(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction(nameof(GetAllSaves));
            }

            var Safes = _unit.SafeRepo.GetsafeByName(name);

            var SafeDtos = _mapper.Map<List<SavesDto>>(Safes);
            return Ok(SafeDtos);
        }
    }
}
