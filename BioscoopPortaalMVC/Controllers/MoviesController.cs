using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BioscoopPortaalMVC.Data;
using BioscoopPortaalMVC.Data.Entities;
using BioscoopPortaalMVC.Models;

namespace BioscoopPortaalMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.Movies.Include(m => m.Director);

            var movies = await _context.Movies.Select(m => new MovieListItemViewModel (){
                    Id = m.Id,
                    Name = m.Name,
                    Director = m.Director.Name,
                    Duration = m.Duration.TotalMinutes.ToString()
            }).ToListAsync();

            return View(movies);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            //Stap 1:
            //Wat is 'ViewData["DirectorsList"]' voor een datatype?
            //Hoe kan ik daarvan een property aanmaken in mijn viewmodel?
            // -> ViewData["DirectorsList"] = new SelectList(_context.Directors, "Id", nameof(Director.Name));
            //Stap 2:
            //Hoe geef ik daarna de directors mee zodat de property ook een lijst van directors bevat?
            // -> SelectList DirectorList = new SelectList(_context.Directors, "Id", nameof(Director.Name));
            // -> var directors = _context.Directors;
            var vmMovie = new MovieUpSertViewModel(_context.Directors.ToList());

            return View(vmMovie);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,ReleaseDate,Duration,DirectorId")] MovieUpSertViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                var movieDataModel = new Movie() { 
                    Name = movieViewModel.Name,
                    Description = movieViewModel.Description,
                    Duration = movieViewModel.Duration,
                    ReleaseDate = movieViewModel.ReleaseDate,
                    DirectorId = movieViewModel.DirectorId
                };
                _context.Add(movieDataModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "Id", movieViewModel.DirectorId);
            movieViewModel.Directors = _context.Directors.ToList();

            return View(movieViewModel);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "Id", movie.DirectorId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ReleaseDate,Duration,DirectorId")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "Id", movie.DirectorId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
