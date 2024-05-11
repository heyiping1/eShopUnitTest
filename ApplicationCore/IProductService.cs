using ApplicationCore.Model.Request;
using ApplicationCore.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore
{
    public interface IProductServiceAsync
    {
        Task<int> SaveProductAsync(ProductRequestModel model);
        Task<IEnumerable<ProductResponseModel>> GetAllProductsAsync();
        Task<ProductResponseModel> GetProductAsync(int id);
    }
}
