using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext: DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }


        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for difficulties
            //East,Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty
                {
                    Id = Guid.Parse("b27fdc88-793c-45df-ae6e-d9cf9a655623"),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("9d4ee3b0-6315-45d7-8ce1-a118655a5eb8"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("df7dd721-b4ad-41e1-89d4-6f61f02c7e98"),
                    Name = "Hard"
                }
                
            };

            //Seed difficulties to the database.
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Seed data for Regions
            var regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.Parse("13b89376-ac4f-4276-8c57-0b35004f1bc3"),
                    Name = "Sevilla",
                    Code = "SEV",
                    RegionImageUrl = "https://www.pexels.com/photo/people-walking-near-the-water-fountain-5470587/"
                },
                new Region
                {
                    Id = Guid.Parse("3a5d6e12-9f67-4d9e-8a9c-1b2c567d8901"),
                    Name = "Barcelona",
                    Code = "BCN",
                    RegionImageUrl = "https://www.pexels.com/photo/beautiful-cityscape-of-barcelona-260330/"
                },
                new Region
                {
                    Id = Guid.Parse("7eae83d0-1e55-4e4d-b8ad-2c56e18b9f8f"),
                    Name = "Paris",
                    Code = "PAR",
                    RegionImageUrl = "https://www.pexels.com/photo/eiffel-tower-338515/"
                },
                new Region
                {
                    Id = Guid.Parse("f4e98e53-6f67-4a3b-ae27-9e8b6d8c6a5f"),
                    Name = "New York City",
                    Code = "NYC",
                    RegionImageUrl = "https://www.pexels.com/photo/time-square-1367277/"
                },

            };
       
            //Getting regopm list data
            modelBuilder.Entity<Region>().HasData(regions);
        
        
        }
    }
}
