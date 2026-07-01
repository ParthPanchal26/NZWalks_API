using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.WalkRepository
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public SQLWalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            _dbContext = nZWalksDbContext;
        }

        public async Task<Walk?> GetExistingWalkByName(string walkName)
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Name == walkName);
        }

        public async Task<Walk?> GetExistingWalkById(Guid id)
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<Walk> CreateWalkAsync(Walk walkModel)
        {
            var walkEntry = await _dbContext.Walks.AddAsync(walkModel);
            await _dbContext.SaveChangesAsync();

            return walkEntry.Entity;
        }

        public async Task<IEnumerable<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null)
        {
            var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }

                if(filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Description.Contains(filterQuery));
                }
            }
            return await walks.ToListAsync();
            //return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> UpdateWalksAsync(Guid id, Walk walkModel)
        {
            var walk = await _dbContext.Walks.FindAsync(id);

            if (walk == null) return null;

            walk.Id = id;
            walk.Name = walkModel.Name;
            walk.Description = walkModel.Description;
            walk.LengthInKm = walkModel.LengthInKm;
            walk.WalkImageUrl = walkModel.WalkImageUrl;
            walk.RegionId = walkModel.RegionId;
            walk.DifficultyId = walkModel.DifficultyId;

            await _dbContext.SaveChangesAsync();

            return await GetExistingWalkById(id);
        }

        public async Task DeleteWalkAsync(Walk walk)
        {
            _dbContext.Walks.Remove(walk);
            await _dbContext.SaveChangesAsync();
        }


    }
}
