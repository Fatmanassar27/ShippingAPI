using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS.OrderItemDTOs;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly IMapper mapper;

        public OrderItemController(UnitOfWork unit , IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult getAllOrdersItems()
        {
            var orderItems = unit.OrderItemRepo.getAllOrderItemsWithOrder();
            if (orderItems == null || orderItems.Count == 0)
            {
                return NotFound("No order items found.");
            }
            List<displayOrderItemDTO> orderItemsDTO = mapper.Map<List<displayOrderItemDTO>>(orderItems);
            return Ok(orderItemsDTO);
        }

        [HttpGet("{id}")]
        public IActionResult getOrderItem(int id) {
            var orderItem = unit.OrderItemRepo.getOrderItemByIdWithOrder(id);
            if (orderItem == null)
            {
                return NotFound($"Order item with ID {id} not found.");
            }
            displayOrderItemDTO orderItemDTO = mapper.Map<displayOrderItemDTO>(orderItem);
            return Ok(orderItemDTO);
        }

        [HttpPost]
        public IActionResult addOrderItem(addOrderItemDTO orderItemDTO)
        {
            if (orderItemDTO == null)
            {
                return BadRequest("Invalid order item data.");
            }
            var orderItem = mapper.Map<OrderItem>(orderItemDTO);
            unit.OrderItemRepo.add(orderItem);
            unit.save();
            displayOrderItemDTO result = mapper.Map<displayOrderItemDTO>(unit.OrderItemRepo.getOrderItemByIdWithOrder(orderItem.Id));
            return Ok(result);
        }

        [HttpPut]
        public IActionResult editOrderItem(int id, addOrderItemDTO orderItemDTO) {
            var orderItem = unit.OrderItemRepo.getOrderItemByIdWithOrder(id);
            if(orderItem == null)
            {
                return NotFound($"Order Item With id {id} not Found");
            }
            mapper.Map(orderItem, orderItemDTO);
            unit.OrderItemRepo.edit(orderItem);
            unit.save();
            displayOrderItemDTO result = mapper.Map<displayOrderItemDTO>(unit.OrderItemRepo.getOrderItemByIdWithOrder(id));
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult deleteOrderItem(int id) {
            var orderItem = unit.OrderItemRepo.getOrderItemByIdWithOrder(id);
            if( orderItem == null)
            {
                return NotFound($"Order Item With id {id} not Found");
            }
            unit.OrderItemRepo.delete(id);
            unit.save();
            return Ok($"Order Item With id {id} deleted Successfully");
        } 
    }
}
