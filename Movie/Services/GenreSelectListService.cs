using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Services
{
    public class GenreSelectListService : IGenreSelectListService
    {
        private readonly MovieContext db;

        public GenreSelectListService(MovieContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<SelectListItem>> GetGenreAsync()
        {
            return await db.Movie
                       .Select(m => m.Genre)
                       .Distinct()
                       .Select(g => new SelectListItem
                       {
                           Text = g.ToString(),
                           Value = g.ToString()
                       })
                       .ToListAsync();
        }
    }
}
