using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Id=1, Name="Smith",Salary=5000});
        }

        [HttpGet("{number:int:max(100)}")]
        public IActionResult Get(int number)
        {
            if (number > 0)
                return Ok("Number is greater than zero");
            return BadRequest("Number is either 0 or less than 0");
            
        }

        //[HttpPost]
        //public IActionResult Post(Product product)
        //{
        //    return Ok(product);
        //}

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            return Ok("REcord with Id ="+id+" is deleted");
        }


        // [HttpDelete("{name}")]
        [HttpDelete]
        public IActionResult Delete(string name)
        {
            return Ok(name + " is deleted");
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult GetBySearch(int id, string name, string department, string location)
        {
            return Ok("your search is working");
        }
    }



}
