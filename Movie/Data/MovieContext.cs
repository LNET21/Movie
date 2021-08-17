using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movie.Models.Entities;

namespace Movie.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext (DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<MovieModel> Movie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieModel>()
               .HasData(
                   new MovieModel { Id = 1, Title = "Titanic", Genre = Genre.Drama, ReleaseDate = DateTime.Parse("1997-02-25"), Rating = 5.0F },
                   new MovieModel { Id = 2, Title = "Screem", Genre = Genre.Horror, ReleaseDate = DateTime.Parse("2005-07-25"), Rating = 2.3F },
                   new MovieModel { Id = 3, Title = "The Shining", Genre = Genre.Horror, ReleaseDate = DateTime.Parse("1997-05-30"), Rating = 4.4F },
                   new MovieModel { Id = 4, Title = "300", Genre = Genre.Action, ReleaseDate = DateTime.Parse("2000-05-30"), Rating = 4.1F },
                   new MovieModel { Id = 5, Title = "Interstellar", Genre = Genre.Drama, ReleaseDate = DateTime.Parse("2014-02-1"), Rating = 4.8F },
                   new MovieModel { Id = 6, Title = "The Dark Knight", Genre = Genre.Action, ReleaseDate = DateTime.Parse("2008-06-12"), Rating = 4.4F }
               );
        }
    }
}
