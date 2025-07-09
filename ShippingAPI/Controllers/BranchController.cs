using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingAPI.DTOS.city_govern;
using ShippingAPI.Models;
using ShippingAPI.UnitOfWorks;

namespace ShippingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly UnitOfWork uow;
        private readonly IMapper map;

        public BranchController(UnitOfWork uow,IMapper map)
        {
            this.uow = uow;
            this.map = map;
        }

        [HttpGet]
        public IActionResult GetAllBranches()
        {
            var branches = uow.BranchRepo.getAll();
            if (branches == null || !branches.Any())
            {
                return NotFound("No branches found.");
            }
            var newbranches = map.Map<List<BranchIDdto>>(branches);
            return Ok(newbranches);
        }
        [HttpGet("{id}")]
        public IActionResult GetBranchById(int id)
        {
            var branch = uow.BranchRepo.getById(id);
            if (branch == null)
            {
                return NotFound();
            }
          
            var newbranch = map.Map<BranchIDdto>(branch); 
            return Ok(newbranch);
        }
        [HttpGet("getbranchbyname/{name}")]
        public IActionResult GetBranchByName(string name)
        {
            var branch = uow.BranchRepo.getByName(name);
            if (branch == null)
            {
                return NotFound("The Branch Is Not Found");
            }
            var newbranch = map.Map<branchDTO>(branch);
            return Ok(newbranch);
        }
        [HttpPost]
        public IActionResult addbrach(branchDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
      var branch = map.Map<Branch>(model);
            if (branch.CityId == 0)
            {
                return BadRequest("City not found");
            }
            uow.BranchRepo.add(branch);
            uow.save();
            return CreatedAtAction(nameof(GetBranchById), new { id = branch.Id }, branch);

        }

        [HttpPut]
        public IActionResult UpdateBranch(BranchIDdto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existbranch = uow.BranchRepo.getById(model.Id);
            if (existbranch == null)
            {
                return NotFound("branch not found");
            }
            var brachbyname = uow.BranchRepo.getByName(model.Name);
            if (brachbyname != null && brachbyname.Id != model.Id)
            {
                return BadRequest("Branch with this name already exists");
            }

            if (model.CityId == 0)
            {
                return BadRequest("City not found");
            }

            map.Map(model, existbranch);
            existbranch.CityId = model.CityId;
            uow.BranchRepo.edit(existbranch);
            uow.save();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult deletebranch(int id)
        {
            var branch = uow.BranchRepo.getById(id);
            if (branch == null)
            {
                return NotFound();
            }
            uow.BranchRepo.delete(id);
            uow.save();
            return NoContent();
        }
    }
}
