using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties{ get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for difficulties
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("c825c1dd-ff1c-4cf4-bb1a-70537656dd6d"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("a0b2fe6c-f29a-4fba-983b-8d6d090260de"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("470a72fd-56c8-447f-bf2e-ea6e5570ab0e"),
                    Name = "Hard"
                },
            };

            // seeds data in database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // seed data for regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("345e16f9-4ef7-4dff-a1f5-e347004cd309"),
                    Code = "NTL",
                    Name = "Northland",
                    RegionImageUrl = "https://picsum.photos/seed/northland/800/600"
                },
                new Region()
                {
                    Id = Guid.Parse("9c79f272-09bd-44d5-89e1-b75c4e0ea4d2"),
                    Code = "AUK",
                    Name = "Auckland",
                    RegionImageUrl = "https://picsum.photos/seed/auckland/800/600"
                },
                new Region()
                {
                    Id = Guid.Parse("de5197f3-6b64-4388-b818-95d5f2e291cd"),
                    Code = "WKO",
                    Name = "Waikato",
                    RegionImageUrl = "https://picsum.photos/seed/waikato/800/600"
                },
                new Region()
                {
                    Id = Guid.Parse("afb206a0-44b7-4cf4-bf5e-40f00a8c3a3a"),
                    Code = "BOP",
                    Name = "Bay of Plenty",
                    RegionImageUrl = "https://picsum.photos/seed/bayofplenty/800/600"
                },
                new Region()
                {
                    Id = Guid.Parse("c5ef0cd4-8070-4ed5-981e-7aebba0e880f"),
                    Code = "GIS",
                    Name = "Gisborne",
                    RegionImageUrl = "https://picsum.photos/seed/gisborne/800/600"
                },
                new Region()
                {
                    Id = Guid.Parse("2c1f4af9-060e-4381-aec9-5bf01615455c"),
                    Code = "HKB",
                    Name = "Hawke's Bay",
                    RegionImageUrl = "https://picsum.photos/seed/hawkesbay/800/600"
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }

    }
}
