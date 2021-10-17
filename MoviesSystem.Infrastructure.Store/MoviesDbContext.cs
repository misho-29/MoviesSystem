using Microsoft.EntityFrameworkCore;
using MoviesSystem.Infrastructure.Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Infrastructure.Store
{
    public class MoviesDbContext : DbContext
    {
        public DbSet<WatchListItem> Watchlist { get; set; }

        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WatchListItem>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.MovieId });
                entity.Property(e => e.IsWatched).IsRequired()
                    .HasDefaultValue(false);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
