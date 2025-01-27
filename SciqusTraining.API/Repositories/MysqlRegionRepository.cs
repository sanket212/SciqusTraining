using Microsoft.EntityFrameworkCore;
using SciqusTraining.API.Data;
using SciqusTraining.API.Models.Domains;

namespace SciqusTraining.API.Repositories
{
    public class MysqlRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public MysqlRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region?> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null) { return null; }
            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion; 
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();

        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingregion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id ==id);
            if (existingregion == null) { return null; }
            existingregion.Code = region.Code;
            existingregion.Name = region.Name;
            existingregion.RegionImageUrl = region.RegionImageUrl;
            await dbContext.SaveChangesAsync();
            return existingregion;
        }
    }
}
