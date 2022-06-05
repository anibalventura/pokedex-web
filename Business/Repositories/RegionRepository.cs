using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Repositories
{
    public class RegionRepository
    {
        private readonly ApplicationContext _dbContext;

        public RegionRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRegion(Region region)
        {
            await _dbContext.Set<Region>().AddAsync(region);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRegion(Region region)
        {
            _dbContext.Entry(region).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRegion(Region region)
        {
            _dbContext.Set<Region>().Remove(region);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Region>> GetAllRegions()
        {
            return await _dbContext
                 .Set<Region>()
                 .ToListAsync();
        }
        public async Task<Region> GetRegionById(int id)
        {
            return await _dbContext
                 .Set<Region>()
                 .FindAsync(id);
        }
    }
}

