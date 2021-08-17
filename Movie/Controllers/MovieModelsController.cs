using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie.Data;
using Movie.Models.Entities;
using Movie.Models.ViewModels;

namespace Movie.Controllers
{
    public class MovieModelsController : Controller
    {
        private readonly MovieContext db;

        public MovieModelsController(MovieContext context)
        {
            db = context;
        }

        // GET: MovieModels
        public async Task<IActionResult> Index()
        {
            return View(await db.Movie.ToListAsync());
        } 
        
        public async Task<IActionResult> Index2()
        {
            var model = new IndexViewModel2
            {
                 Movies = await db.Movie.ToListAsync(),
                 Genres = await GetGenresAsync()
            };

            return View(model);
        }  
        
        public async Task<IActionResult> Index3()
        {
            var movies = await db.Movie.ToListAsync();

            var model = new IndexViewModel3
            {
                 Movies = movies
            };

            return View(model);
        }

        private async Task<IEnumerable<SelectListItem>> GetGenresAsync()
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

        public async Task<IActionResult> Filter(string title, int? genre)
        {

            var model = string.IsNullOrWhiteSpace(title) ?
                            db.Movie :
                            db.Movie.Where(m => m.Title.StartsWith(title));

            model = genre == null ?
                            model :
                            model.Where(m => (int)m.Genre == genre);

            return View(nameof(Index), await model.ToListAsync());
        } 
        
        public async Task<IActionResult> Filter2(IndexViewModel2  viewModel)
        {

            var movies = string.IsNullOrWhiteSpace(viewModel.Title) ?
                            db.Movie :
                            db.Movie.Where(m => m.Title.StartsWith(viewModel.Title));

            movies = viewModel.Genre == null ?
                            movies :
                            movies.Where(m => m.Genre == viewModel.Genre);

            var model = new IndexViewModel2
            {
                Movies = await movies.ToListAsync(),
                Genres = await GetGenresAsync()
            };

            return View(nameof(Index2), model);
        } 
        
        public async Task<IActionResult> Filter3(IndexViewModel3  viewModel)
        {

            var movies = string.IsNullOrWhiteSpace(viewModel.Title) ?
                            db.Movie :
                            db.Movie.Where(m => m.Title.StartsWith(viewModel.Title));

            movies = viewModel.Genre == null ?
                            movies :
                            movies.Where(m => m.Genre == viewModel.Genre);

            var model = new IndexViewModel3
            {
                Movies = await movies.ToListAsync()
            };

            return View(nameof(Index3), model);
        }




        // GET: MovieModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieModel = await db.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieModel == null)
            {
                return NotFound();
            }

            return View(movieModel);
        }

        // GET: MovieModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovieModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Rating")] MovieModel movieModel)
        {
            if (ModelState.IsValid)
            {
                db.Add(movieModel);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movieModel);
        }

        // GET: MovieModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieModel = await db.Movie.FindAsync(id);
            if (movieModel == null)
            {
                return NotFound();
            }
            return View(movieModel);
        }

        // POST: MovieModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Rating")] MovieModel movieModel)
        {
            if (id != movieModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(movieModel);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieModelExists(movieModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movieModel);
        }

        // GET: MovieModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieModel = await db.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieModel == null)
            {
                return NotFound();
            }

            return View(movieModel);
        }

        // POST: MovieModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieModel = await db.Movie.FindAsync(id);
            db.Movie.Remove(movieModel);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieModelExists(int id)
        {
            return db.Movie.Any(e => e.Id == id);
        }
    }
}
