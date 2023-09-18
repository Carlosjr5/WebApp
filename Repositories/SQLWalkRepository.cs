using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        //declare database context.
        private readonly NZWalksDbContext dbContext;

        //contructor
        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
            
        }


        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
                                                   string? sortBy = null, bool isAscending = true,
                                                      int pageNumber = 1, int pageSize = 1000)
        {
            //Querying Walks
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false )
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }

            }

            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    //If ascending order by A-Z
                    walks = isAscending ? walks.OrderBy(x => x.Name):
                            //Order Z-A
                            walks.OrderByDescending(x => x.Name);
                }else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    //Changin the walks order depending on km.
                    walks = isAscending  ? walks.OrderBy(x => x.LengthInKm) :
                                           walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //Pagination
            var skipResults = (pageNumber -1) * pageSize;


            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();


            //  return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
          return  await dbContext.Walks.
                Include("Difficulty").
                Include("Region").
                FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
         var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id); 

            if(existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.Region = walk.Region;

            await dbContext.SaveChangesAsync();

            return existingWalk;

        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
         var existingWalk=   await dbContext.Walks.FirstOrDefaultAsync(x=> x.Id == id);

            if(existingWalk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(existingWalk);
            await  dbContext.SaveChangesAsync();
            return existingWalk;

        }



    }


}
