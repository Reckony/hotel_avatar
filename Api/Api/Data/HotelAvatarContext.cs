using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class HotelAvatarContext : DbContext
    {
        public HotelAvatarContext(DbContextOptions<HotelAvatarContext> options)
            : base(options)
        {
        }

        public DbSet<Reservations> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservations>().ToTable("Reservations");
        }
    }
}
