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
    public class CitiesController : ControllerBase
    {
        private readonly UnitOfWork uow;
        private readonly IMapper map;

        public CitiesController(UnitOfWork Uow ,IMapper map)
        {
            uow = Uow;
            this.map = map;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cities = uow.CityRepo.getAll();
            
            if (cities == null || !cities.Any())
            {
                return NotFound("There Are Not Cities!");
            }
            var newcities = map.Map<List<cityidDTO>>(cities);
            return Ok(newcities);
        }

        [HttpGet("{id:int}")]
        public IActionResult getcitybyid(int id)
        {
            var city = uow.CityRepo.getById(id);
            if (city==null)
            {
                return NotFound(" The City Is Not Found");
            }
            var cityDto = map.Map<cityDTO>(city);
            return Ok(cityDto);
        }

        [HttpGet("getcitybyname/{name}")]
        public IActionResult getcitybyname(string name)
        {
            var city = uow.CityRepo.getByName(name);
            if (city == null)
            {
                return NotFound(" The City Is Not Found");
            }
            var cityDto = map.Map<cityDTO>(city);
            return Ok(cityDto);
        }

        [HttpPost]
        public IActionResult addCity(cityDTO cityDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cityExists = uow.CityRepo.getByName(cityDto.Name);
            if (cityExists != null)
                return BadRequest("City already exists");

            var governorate = uow.GovernateRepo.getById(cityDto.GovernorateId);
            if (governorate == null)
                return BadRequest("Governorate not found");

            var city = map.Map<City>(cityDto);
            uow.CityRepo.add(city);
            uow.save();

            return Ok(new { message = "City Added Successfully" });
        }




        [HttpPut]
        public IActionResult editCity(cityidDTO cityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCity = uow.CityRepo.getById(cityDto.Id);
            if (existingCity == null)
            {
                return NotFound("City not found");
            }
            // دوري على المدينة بالاسم
            var cityByName = uow.CityRepo.getByName(cityDto.Name);
            if (cityByName != null && cityByName.Id != cityDto.Id)
            {
                return BadRequest("City with this name already exists");
            }

            var governorate = uow.GovernateRepo.getByName(cityDto.GoverrateName);
            if (governorate == null)
            {
                return BadRequest("Governorate not found");
            }

            // ماب التعديلات على الكائن الموجود
            map.Map(cityDto, existingCity);

            // اربطي الـ GovernorateId يدوي
            existingCity.GovernorateId = governorate.Id;

            uow.cityRepo.edit(existingCity);
            uow.save();

            return Ok(new { message = "City Updated Successfully" });
        }
        [HttpPut("togglestatusbyname/{name}")]
        public IActionResult ToggleCityStatusByName(string name)
        {
            var city = uow.CityRepo.getByName(name);
            if (city == null)
                return NotFound("City not found.");

            city.IsActive = !city.IsActive;

            uow.CityRepo.edit(city);
            uow.save();

            return Ok(new { message = "City status updated successfully", isActive = city.IsActive });
        }




        [HttpDelete("{id:int}")]
        public IActionResult deleteCity(int id)
        {
            try
            {
                var city = uow.CityRepo.getById(id);
                if (city == null)
                {
                    return NotFound("City not found");
                }

                uow.cityRepo.delete(id);
                uow.save();

                return Ok(new { message = "City Deleted Successfully" });
            }
            catch (Exception ex)
            {
                // ممكن نضيف لوج هنا لاحقًا
                return BadRequest(new { message = "Can't delete this city because it has related data." });
            }
        }


        [HttpGet("getbygovernorateid/{governorateId:int}")]
        public IActionResult getByGovernorateId(int governorateId)
        {
            var cities = uow.CityRepo.getByGovernorateId(governorateId);
            if (cities == null || !cities.Any())
            {
                return NotFound("There Are Not Cities For This Governorate!");
            }
            var newcities = map.Map<List<cityidDTO>>(cities);
            return Ok(newcities);
        }


        }
}
