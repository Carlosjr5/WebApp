using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        //Definition for Get all.
        Task<List<Region>> GetAllAsync();

        //Definition for Get by ID.
        Task<Region?> GetByIdAsync(Guid id);

        //Definition for Create.
        Task<Region>CreateAsync(Region region);

        //Definition for Update.
        Task<Region?>UpdateAsync(Guid id, Region region);

        //Definition for Delete.
        Task<Region?> DeleteAsync(Guid id);
    }
}
