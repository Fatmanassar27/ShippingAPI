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
            var newcities = map.Map<List<cityDTO>>(cities);
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
            {
                return BadRequest(ModelState);
            }

            // دوري على المحافظة بالاسم
            var governorate = uow.GovernateRepo.getByName(cityDto.GoverrateName);
            if (governorate == null)
            {
                return BadRequest("Governorate not found");
            }

            // اعملي الماب
            var city = map.Map<City>(cityDto);

            // اربطي الـ GovernorateId يدوي
            city.GovernorateId = governorate.Id;

            uow.CityRepo.add(city);
            uow.save();

            return Ok("City Added Successfully");
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

            return Ok("City Updated Successfully");
        }


        [HttpDelete("{id:int}")]
        public IActionResult deleteCity(int id)
        {
            var city = uow.CityRepo.getById(id);
            if (city == null)
            {
                return NotFound("City not found");
            }
            uow.cityRepo.delete(id);
            uow.save();
            return Ok("City Deleted Successfully");
        }


    }
}
