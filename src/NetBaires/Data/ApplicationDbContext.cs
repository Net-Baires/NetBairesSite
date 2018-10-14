using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NetBaires.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<EventDb> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<SponsorDb> Sponsors { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpeakerEvent>()
                .HasKey(a => new { a.SpeakerId, a.EventId });
            modelBuilder.Entity<SponsorEvent>()
                .HasKey(a => new { a.SponsorId, a.EventId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
