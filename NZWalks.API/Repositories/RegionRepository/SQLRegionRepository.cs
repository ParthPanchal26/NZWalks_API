using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.RegionDTOs;

namespace NZWalks.API.Repositories.RegionRepository
{
    public class SQLRegionRepository: IRegionRepository
    {
        private readonly NZWalksDbContext _dbContext;
        
        public SQLRegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            _dbContext = nZWalksDbContext;
        }

        // Get all the regions from db
        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        // Get a single region using its id
        public async Task<Region?> GetRegionAsync(Guid id)
        {
            return await _dbContext.Regions.FindAsync(id);
        }

        // Check the exising region using its name or code
        public async Task<Region?> GetExisingRegion(string RegionName, string RegionCode)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(r => r.Name == RegionName || r.Code == RegionCode);
        }

        // Create a new region
        public async Task<Region> CreateRegionAsync(Region region)
        {
            var regionEntry = await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();

            return regionEntry.Entity;
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region exisingRegion, UpdateRegionRequestDTO model)
        {
            exisingRegion.Name = model.Name;
            exisingRegion.Code = model.Code;
            exisingRegion.RegionImageUrl = model.RegionImageUrl;

            await _dbContext.SaveChangesAsync();

            return exisingRegion;
        }

        public async Task DeleteRegionAsync(Region region)
        {
            _dbContext.Regions.Remove(region);
            await _dbContext.SaveChangesAsync();
        }

    }
}
