using ApplicationCore.Entities;
using ApplicationCore.RepositoryContracts;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CategoryRepositoryAsync : BaseRepositoryAsync<Category>, ICategoryRepositoryAsync
    {
        public CategoryRepositoryAsync(EcommerceDbContext c) : base(c)
        {
        }
    }
}
