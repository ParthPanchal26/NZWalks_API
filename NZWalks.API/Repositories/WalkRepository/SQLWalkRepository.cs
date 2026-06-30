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
            await _dbContext.Walks.AddAsync(walkModel);
            await _dbContext.SaveChangesAsync();

            return await GetExistingWalkByName(walkModel.Name);
        }

        public async Task<IEnumerable<Walk>> GetAllWalksAsync()
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
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
