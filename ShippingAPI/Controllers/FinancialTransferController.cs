using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS.FinancialTransferDtOs;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialTransferController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly IMapper mapper;

        public FinancialTransferController(UnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddTransfer([FromBody] FinancialTransferDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = mapper.Map<FinancialTransfer>(dto);

            // Get the current admin ID from the token
            var adminId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            entity.AdminId = adminId;

            var success = await unit.FinancialTransferRepo.AddTransferAsync(entity);

            if (!success)
                return StatusCode(500, "An error occurred while processing the transaction.");

            return Ok(new { message = "Transfer completed successfully." });
        }
        [HttpGet("banks")]
        public IActionResult GetBankTransfers([FromQuery] string? bankName = null,[FromQuery] DateTime? startDate = null,[FromQuery] DateTime? endDate = null)
        {
            var transfers = unit.FinancialTransferRepo.GetBankTransfersFiltered(bankName, startDate, endDate);
            var result = new List<BankTransactionReportDto>() { };
            if (!transfers.Any())
                return Ok(result);

            result = mapper.Map<List<BankTransactionReportDto>>(transfers);
            return Ok(result);
        }
        [HttpGet("safes")]
        public IActionResult GetSafeTransfers([FromQuery] string? safeName = null,[FromQuery] DateTime? startDate = null,[FromQuery] DateTime? endDate = null)
        {
            var transfers = unit.FinancialTransferRepo.GetSafeTransfersFiltered(safeName, startDate, endDate);
            var result = new List<SafeTransactionReportDto>() { };
            if (!transfers.Any())
                return Ok(result);

            result = mapper.Map<List<SafeTransactionReportDto>>(transfers);
            return Ok(result);
        }

    }
}
