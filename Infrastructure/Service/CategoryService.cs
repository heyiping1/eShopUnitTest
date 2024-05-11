using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.Model.Request;
using ApplicationCore.Model.Response;
using ApplicationCore.RepositoryContracts;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class CategoryServiceAsync : ICategoryServiceAsync
    {
        private readonly ICategoryRepositoryAsync categoryRepository;
        public CategoryServiceAsync(ICategoryRepositoryAsync repo)
        {
            categoryRepository = repo;
        }


        public async Task<int> DeleteCategoryAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception($"Category with Id {id} not found");
            }
            return await categoryRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CategoryResponseModel>> GetAllCategoriesAsync()
        {
            List<CategoryResponseModel> lst= new List<CategoryResponseModel>();
            var collection = await categoryRepository.GetAllAsync();
            if(collection != null)
            {
                foreach (var item in collection)
                {
                    CategoryResponseModel m = new CategoryResponseModel();
                    m.Id= item.Id;
                    m.CategoryName = item.CategoryName;
                    lst.Add(m);
                }
                return lst;
            }
            return null;
        }

        public async Task<CategoryResponseModel> GetCategoryByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category != null)
            {
             CategoryResponseModel categoryResponseModel = new CategoryResponseModel();
                categoryResponseModel.Id = category.Id;
                categoryResponseModel.CategoryName = category.CategoryName;
                return categoryResponseModel;
            }
            return null;
        }

        public async Task<int> InsertCategoryAsync(CategoryRequestModel category)
        {
            Category c = new Category() {
                CategoryName = category.CategoryName
                
            };

           return await categoryRepository.InsertAsync(c);
        }

        public async Task<int> UpdateCategoryAsync(CategoryRequestModel category, int id)
        {
            Category c = new Category() { 
            Id=id,
            CategoryName = category.CategoryName
            };
            return await categoryRepository.UpdateAsync(c);
        }
    }
}
