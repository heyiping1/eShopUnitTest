using ApplicationCore.Model.Request;
using ApplicationCore.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore
{
    public interface ICategoryServiceAsync
    {
        Task<IEnumerable<CategoryResponseModel>> GetAllCategoriesAsync();
        Task<int> InsertCategoryAsync(CategoryRequestModel category);
        Task<int> UpdateCategoryAsync(CategoryRequestModel category, int id);
        Task<int> DeleteCategoryAsync(int id);
        Task<CategoryResponseModel> GetCategoryByIdAsync(int id);
    }
}
