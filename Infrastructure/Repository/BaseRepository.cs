using ApplicationCore.RepositoryContracts;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class BaseRepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly EcommerceDbContext _context;
        public BaseRepositoryAsync(EcommerceDbContext c)
        {
            _context = c;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var i = await GetByIdAsync(id);
            if (i != null)
            {
                _context.Set<T>().Remove(i);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //.....
            //....
            return await _context.Set<T>().ToListAsync(); ;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<int> InsertAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _context.Set<T>().Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
