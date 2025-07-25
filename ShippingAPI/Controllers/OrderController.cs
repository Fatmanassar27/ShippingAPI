﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS.OrderDTOs;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;
using System.Security.Claims;

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

        [HttpGet("getAllOrders")]
        public IActionResult getAllOrders()
        {
            var orders = unit.OrderRepo.getAllWithObj();
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
            order.TotalCost = unit.OrderRepo.CalculateTotalCost(order);
            unit.OrderRepo.add(order);
            unit.save();
            displayOrderDTO result = mapper.Map<displayOrderDTO>(unit.OrderRepo.getByIdWithObj(order.Id));
            return Ok(new { result  , id=order.Id});
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

            var oldStatus = order.Status;
            mapper.Map(orderDTO, order);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (order.Status != oldStatus)
            {
                var history = new OrderStatusHistory
                {
                    OrderId = order.Id,
                    OldStatus = oldStatus,
                    NewStatus = order.Status,
                    ChangedByUserId = userId, 
                    Notes = $"Status updated via admin panel to {order.Status}",
                    ChangedAt = DateTime.UtcNow
                };
                unit.context.OrderStatusHistories.Add(history);
            }

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

        [HttpGet("getByCourier/{courierId}")]
        public IActionResult getOrdersByCourier(string courierId)
        {
            var orders = unit.OrderRepo.getAllByCourierId(courierId);
            if (orders == null || !orders.Any())
            {
                return NotFound($"No Orders Found for Courier with ID {courierId}");
            }
            List<displayOrderDTO> result = mapper.Map<List<displayOrderDTO>>(orders);
            return Ok(result);
        }

        [HttpGet("getByBranchId/{branchId}")]
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

        [HttpGet("getByGovId/{govId}")]
        public IActionResult getOrdersByGovernorate(int govId)
        {
            var orders = unit.OrderRepo.getAllByGovId(govId);
            if (orders == null || !orders.Any())
            {
                return NotFound($"No Orders Found for Branch with ID {govId}");
            }
            List<displayOrderDTO> result = mapper.Map<List<displayOrderDTO>>(orders);
            return Ok(result);
        }

        [HttpGet("getByCityId/{cityId}")]
        public IActionResult getOrdersByCity(int cityId)
        {
            var orders = unit.OrderRepo.getAllByCityId(cityId);
            if (orders == null || !orders.Any())
            {
                return NotFound($"No Orders Found for Branch with ID {cityId}");
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

        [HttpGet("ByDate")]
        public IActionResult GetOrdersByDateRange([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            if (fromDate > toDate)
                return BadRequest("Start date must be before end date.");

            var orders = unit.OrderRepo.GetOrdersByDateRange(fromDate, toDate);
            var result = mapper.Map<List<displayOrderDTO>>(orders);

            return Ok(result);
        }


    }
}
