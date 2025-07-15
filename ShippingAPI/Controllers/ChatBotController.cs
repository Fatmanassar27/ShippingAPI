using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShippingAPI.Data;
using ShippingAPI.Models;
using System.Text.Json;

namespace ShippingAPI.Controllers
{
    public class ChatRequest
    {
        public string Question { get; set; }
    }

    public class StaticBotResponse
    {
        public List<string> Keywords { get; set; }
        public string responseEn { get; set; }
        public string responseAr { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotController : ControllerBase
    {
        private readonly ShippingContext _context;

        public ChatBotController(ShippingContext context)
        {
            _context = context;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] ChatRequest request)
        {
            var question = request.Question.ToLower();
            string response;
            response = await CheckStaticResponses(question);
            if (!string.IsNullOrEmpty(response))
                return Ok(new { response });
            if (question.Contains("rejected"))
            {
                var grouped = await _context.Orders
                    .Where(o => o.Status == OrderStatus.RejectedWithoutPayment
                             || o.Status == OrderStatus.RejectedWithPartialPayment
                             || o.Status == OrderStatus.RejectedWithPayment)
                    .GroupBy(o => o.RejectionReason)
                    .Select(g => new { Reason = g.Key, Count = g.Count() })
                    .ToListAsync();

                response = grouped.Count == 0
                    ? "There are no rejected orders at the moment."
                    : "Rejected Orders:\n" + string.Join("\n", grouped.Select(g =>
                        $"{g.Count} orders were rejected due to: {g.Reason}"));
            }
            else if (question.Contains("orders") && question.Contains("in delivery"))
            {
                var count = await _context.Orders.CountAsync(o => o.Status == OrderStatus.PartiallyDelivered);
                response = $"There are currently {count} orders in delivery.";
            }
            else
            {
                response = "Sorry, I couldn't understand your question. Please rephrase it.";
            }

            return Ok(new { response });
        }

        private async Task<string> CheckStaticResponses(string question)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "BotStaticResponses.json");

            if (!System.IO.File.Exists(path))
            {
                Console.WriteLine("❌ File not found at path: " + path);
                return null;
            }

            var json = await System.IO.File.ReadAllTextAsync(path);
            Console.WriteLine("📄 JSON loaded");

            List<StaticBotResponse> responses;
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                responses = JsonSerializer.Deserialize<List<StaticBotResponse>>(json, options);

                if (responses == null)
                {
                    Console.WriteLine("❌ Deserialization returned null");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Deserialization error: " + ex.Message);
                return null;
            }

            foreach (var item in responses)
            {
                if (item.Keywords == null) continue;

                foreach (var keyword in item.Keywords)
                {
                    if (question.Contains(keyword.ToLower()))
                    {
                        bool isArabic = keyword.Any(c => c >= 0x0600 && c <= 0x06FF);
                        return isArabic ? item.responseAr : item.responseEn;
                    }
                }
            }

            return null;
        }


    }
}
