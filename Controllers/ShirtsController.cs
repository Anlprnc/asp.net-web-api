using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Filters.ExceptionFilters;
using WebAPI.Models;
using WebAPI.Models.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(ShirtRepository.GetShirts());
        }

        [HttpGet("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult GetShirtById(int id)
        {
            return Ok(ShirtRepository.GetShirtById(id));
        }

        [HttpPost]
        [Shirt_ValidateCreateShirtFilter]
        public IActionResult Createshirt([FromBody] Shirt shirt)
        {
            ShirtRepository.AddShirt(shirt);

            return CreatedAtAction(nameof(GetShirtById), new { id = shirt.ShirtId }, shirt);
        }

        [HttpPut("{id}")]
        [Shirt_ValidateShirtIdFilter]
        [Shirt_ValidateUpdateShirtFilter]
        [Shirt_HandleUpdateExceptionsFilter]
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
            ShirtRepository.UpdateShirt(shirt);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult DeleteShirt(int id)
        {
            var shirt = ShirtRepository.GetShirtById(id);
            ShirtRepository.DeleteShirt(id);

            return Ok(shirt);
        }
    }
}
