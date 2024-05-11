using ApplicationCore;
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
            return Ok(await categoryServiceAsync.InsertCategoryAsync(categoryRequestModel));
        }

        [HttpPut]
        public async Task<IActionResult> Put(CategoryRequestModel model, int id)
        { 
          return Ok(await categoryServiceAsync.UpdateCategoryAsync(model, id));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await categoryServiceAsync.DeleteCategoryAsync(id));
        }
    }
}
