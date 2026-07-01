using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.WalkRepository
{
    public interface IWalkRepository
    {
        Task<Walk?> GetExistingWalkByName(string walkName);
        Task<Walk?> GetExistingWalkById(Guid id);
        Task<Walk> CreateWalkAsync(Walk walkModel);
        Task<IEnumerable<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> UpdateWalksAsync(Guid id, Walk walkModel);
        Task DeleteWalkAsync(Walk walk);
    }
}
