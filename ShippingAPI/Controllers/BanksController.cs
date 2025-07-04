using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.Data;
using ShippingAPI.DTOS;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly UnitOfWork _unit;
        private readonly IMapper _mapper;
        public BanksController(IMapper mapper, UnitOfWork unit)
        {
            _unit = unit;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllBanks()
        {
            var banks = _unit.BankRepo.getAll();
            if (banks == null || !banks.Any())
            {
                return NotFound("No banks found.");
            }
            var result = _mapper.Map<List<BankDTO>>(banks);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddBank([FromBody] BankDTO bankDTO)
        {
            if (bankDTO == null)
            {
                return BadRequest("Invalid bank data.");
            }
            var bank = _mapper.Map<Bank>(bankDTO);
            _unit.BankRepo.add(bank);
            _unit.save();
            return CreatedAtAction(nameof(GetAllBanks), new { id = bank.Id }, bank);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBank(int id, [FromBody] BankDTO bankDTO)
        {
            if (bankDTO == null || id != bankDTO.Id)
            {
                return BadRequest("Invalid bank data.");
            }
            var existingBank = _unit.BankRepo.getById(id);
            if (existingBank == null)
            {
                return NotFound($"Bank with ID {id} not found.");
            }
            existingBank.Name = bankDTO.Name;
            existingBank.BranchId = bankDTO.BranchId;
            existingBank.Balance = bankDTO.Balance;
            existingBank.IsActive = bankDTO.IsActive;
            existingBank.CreatedDate = bankDTO.CreatedDate;
            _unit.BankRepo.edit(existingBank);
            _unit.save();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBank(int id)
        {
            var existingBank = _unit.BankRepo.getById(id);
            if (existingBank == null)
            {
                return NotFound($"Bank with ID {id} not found.");
            }
            _unit.BankRepo.delete(id);
            _unit.save();
            return NoContent();
        }
        [HttpGet("{id}")]
        public IActionResult GetBankById(int id)
        {
            var existingBank = _unit.BankRepo.getById(id);
            if (existingBank == null)
            {
                return NotFound($"Bank with ID {id} not found.");
            }
            var bankDTO = _mapper.Map<BankDTO>(existingBank);
            return Ok(bankDTO);
        }
        [HttpPost("{id}/active")]
        public IActionResult ActivatedBank(int id)
        {
            var existingBank = _unit.BankRepo.getById(id);
            if (existingBank == null)
            {
                return NotFound($"Bank with ID {id} not found.");
            }
            if (existingBank.IsActive)
            {
                return Ok("Bank is already active.");
            }
            existingBank.IsActive = true;
            _unit.BankRepo.edit(existingBank);
            _unit.save();
            return Ok("Bank activated successfully.");
        }
        [HttpPost("{id}/disactive")]
        public IActionResult DisActivatedBank(int id)
        {
            var existingBank = _unit.BankRepo.getById(id);
            if (existingBank == null)
            {
                return NotFound($"Bank with ID {id} not found.");
            }
            if (!existingBank.IsActive)
            {
                return Ok("Bank is already disactive.");
            }
            existingBank.IsActive = false;
            _unit.BankRepo.edit(existingBank);
            _unit.save();
            return Ok("Bank disactivated successfully.");
        }

        [HttpGet("search")]
        public IActionResult SearchBanks(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
               return RedirectToAction(nameof(GetAllBanks));
            }

            var banks = _unit.BankRepo.GetBanksByName(name);

            var bankDtos = _mapper.Map<List<BankDTO>>(banks);
            return Ok(bankDtos);
        }
    }

}
