using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.Model.Request;
using ApplicationCore.Model.Response;
using ApplicationCore.RepositoryContracts;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class ProductServiceAsync : IProductServiceAsync
    {
        private readonly IProductRepositoryAsync _repository;
        private readonly IMapper _mapper;
        public ProductServiceAsync(IProductRepositoryAsync productRepository, IMapper mapper)
        {
                _repository = productRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductResponseModel>> GetAllProductsAsync()
        {
            var products =  await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductResponseModel>>(products);
        }

        public async Task< ProductResponseModel> GetProductAsync(int id)
        {
            var product = _repository.GetByIdAsync(id);
            if (product != null)
            {
                return _mapper.Map<ProductResponseModel>(product);
            }
            return null;
        }

        public async Task<int> SaveProductAsync(ProductRequestModel model)
        {
            var product = _mapper.Map<Product>(model);
            return await _repository.InsertAsync(product);
        }
    }
}
