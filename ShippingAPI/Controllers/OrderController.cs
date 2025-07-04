using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS.OrderDTOs;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IMapper mapper;
        UnitOfWork unit;

        public OrderController(UnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult getAllOrders()
        {
            var orders = unit.OrderRepo.getAll();
            if (orders == null || !orders.Any())
            {
                return NotFound("No Orders Founded");
            }
            List<displayOrderDTO> result = mapper.Map<List<displayOrderDTO>>(orders);
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult getOrderById(int id)
        {
            var order = unit.OrderRepo.getByIdWithObj(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            displayOrderDTO result = mapper.Map<displayOrderDTO>(order);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult addOrder(addOrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                return BadRequest("Invalid Order data");
            }
            var order = mapper.Map<Order>(orderDTO);
            unit.OrderRepo.add(order);
            unit.save();
            displayOrderDTO result = mapper.Map<displayOrderDTO>(unit.OrderRepo.getByIdWithObj(order.Id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult updateOrder(int id, addOrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                return BadRequest("Invalid Order data");
            }
            var order = unit.OrderRepo.getByIdWithObj(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            mapper.Map(orderDTO, order);
            unit.OrderRepo.edit(order);
            unit.save();
            displayOrderDTO result = mapper.Map<displayOrderDTO>(unit.OrderRepo.getByIdWithObj(order.Id));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult deleteOrder(int id)
        {
            var order = unit.OrderRepo.getByIdWithObj(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            unit.OrderRepo.delete(id);
            unit.save();
            return Ok($"Order with ID {id} deleted successfully.");
        }

        [HttpGet("getByTrader/{traderId}")]
        public IActionResult getOrdersByTrader(string traderId)
        {
            var orders = unit.OrderRepo.getAllByTraderId(traderId);
            if (orders == null || !orders.Any())
            {
                return NotFound($"No Orders Found for Trader with ID {traderId}");
            }
            List<displayOrderDTO> result = mapper.Map<List<displayOrderDTO>>(orders);
            return Ok(result);
        }

        [HttpGet("getByBranch/{branchId}")]
        public IActionResult getOrdersByBranch(int branchId)
        {
            var orders = unit.OrderRepo.getAllByBranchId(branchId);
            if (orders == null || !orders.Any())
            {
                return NotFound($"No Orders Found for Branch with ID {branchId}");
            }
            List<displayOrderDTO> result = mapper.Map<List<displayOrderDTO>>(orders);
            return Ok(result);
        }

        [HttpGet("getByStatus/{status}")]
        public IActionResult getOrdersByStatus(OrderStatus status)
        {
            var orders = unit.OrderRepo.getAllByStatus(status);
            if (orders == null || !orders.Any())
            {
                return NotFound($"No Orders Found with Status {status}");
            }
            List<displayOrderDTO> result = mapper.Map<List<displayOrderDTO>>(orders);
            return Ok(result);
        }


    }
}
