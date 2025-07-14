using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS.Permissions;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionActionController : ControllerBase
    {
        private readonly UnitOfWork uow;
        private readonly IMapper mapper;

        public PermissionActionController(UnitOfWork uow,IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }


    
        [HttpGet]
        public IActionResult GetAll()
        {
            var peractions = uow.PermissionActionRepo.getAllWithIncludes(); // هقولك تعمل إيه هنا تحت

            if (peractions == null || !peractions.Any())
                return NotFound("There is no data!");

            var mapped = mapper.Map<List<Permissionactioncreate>>(peractions);
            return Ok(mapped);
        }


    }
}
