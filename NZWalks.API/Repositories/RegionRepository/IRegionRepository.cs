using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.RegionDTOs;

namespace NZWalks.API.Repositories.RegionRepository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionsAsync();
        Task<Region?> GetRegionAsync(Guid id);
        Task<Region?> GetExisingRegion(string RegionName, string RegionCode);
        Task<Region> CreateRegionAsync(Region region);
        Task<Region> UpdateRegionAsync(Guid id, Region exisingRegion, UpdateRegionRequestDTO model);
        Task DeleteRegionAsync(Region region);
    }
}
