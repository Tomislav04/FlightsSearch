using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data
{
    public class FlightSearchContext : DbContext
    {
        public FlightSearchContext(DbContextOptions<FlightSearchContext> options) : base(options) { }

        public DbSet<FlightSearchRequest> FlightSearchRequests { get; set; }
        public DbSet<FlightSearchResult> FlightSearchResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FlightSearchRequest>()
                .HasMany(fsr => fsr.Results)
                .WithOne()
                .HasForeignKey(fr => fr.FlightSearchRequestId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
