using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.Model.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServiceAsync categoryServiceAsync;
        public CategoryController(ICategoryServiceAsync categoryServiceAsync)
        {
                this.categoryServiceAsync = categoryServiceAsync;
        }
        [Authorize (AuthenticationSchemes= JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> Get()
        { 

         return Ok(await categoryServiceAsync.GetAllCategoriesAsync());
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(CategoryRequestModel categoryRequestModel)
        {
            try
            {
                var response = await categoryServiceAsync.InsertCategoryAsync(categoryRequestModel);
                return Ok(response);
            } catch (Exception e)
            {
                return BadRequest("Arguments are not valid.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(CategoryRequestModel model, int id)
        { 
            try
            {
                var response = await categoryServiceAsync.UpdateCategoryAsync(model, id);
                return Ok(response);
            } catch (Exception e)
            {
                return BadRequest("Arguments are not valid.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await categoryServiceAsync.DeleteCategoryAsync(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest($"Category with Id {id} not found");
            }
        }
    }
}
