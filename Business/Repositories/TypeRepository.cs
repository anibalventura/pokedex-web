using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Repositories
{
	public class TypeRepository
	{
		private readonly ApplicationContext _dbContext;

		public TypeRepository(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}

        public async Task AddType(Type type)
        {
            await _dbContext.Set<Type>().AddAsync(type);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateType(Type type)
        {
            _dbContext.Entry(type).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteType(Type type)
        {
            _dbContext.Set<Type>().Remove(type);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Type>> GetAllTypes()
        {
            return await _dbContext
                 .Set<Type>()
                 .ToListAsync();
        }
        public async Task<Type> GetTypeById(int id)
        {
            return await _dbContext
                 .Set<Type>()
                 .FindAsync(id);
        }
    }
}

