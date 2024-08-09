using Microsoft.EntityFrameworkCore;
using ResidentWebApp.Models;
using ResidentWebApp.Data;
namespace ResidentWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // Configure CenterID as the primary key for EvacuationCenter entity
             modelBuilder.Entity<EvacuationCenter>()
                .HasKey(e => e.CenterID);

                modelBuilder.Entity<User>()
            .ToTable("Resident") // or "Resident" if that's your actual table name
            .HasKey(u => u.Id); // or Id, match with your model

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
            
             modelBuilder.Entity<EvacuationRoom>()
            .HasKey(r => r.RoomID); // Define the primary key

           modelBuilder.Entity<NewsEvents>()
    .HasKey(ne => ne.EventID);

            modelBuilder.Entity<HazardMap>()
            .HasKey(h => h.MapID);

            modelBuilder.Entity<Video>()
            .HasKey(v => v.VideoID);
             modelBuilder.Entity<Tip>()
            .HasKey(v => v.TextID);

            modelBuilder.Entity<Disaster>()
            .HasKey(v => v.DisasterID);


        base.OnModelCreating(modelBuilder);
        }

        public DbSet<EvacuationCenter> EvacuationCenters { get; set; }
         public DbSet<User> Users { get; set; }
         public DbSet<EvacuationRoom> EvacuationRooms { get; set; }
         public DbSet<NewsEvents> NewsEvents { get; set; }
         public DbSet<HazardMap> HazardMaps { get; set; }
         public DbSet<Video> Videos { get; set; }
         public DbSet<Disaster> Disasters { get; set; }
         public DbSet<Tip> TutorialTexts { get; set; }

        
    }

    
}
