using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MVCMovie.Models;
using MVCMovie.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MVCMovie.Controllers;

public class MoviesController : Controller
{
    private readonly MovieContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    // Inject MovieContext and UserManager
    public MoviesController(MovieContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Movies
    [Authorize]
    public async Task<IActionResult> Index(string? searchString)
    {
        ViewData["IsSearch"] = !string.IsNullOrWhiteSpace(searchString);
        ViewData["SearchQuery"] = searchString;

        if (!string.IsNullOrEmpty(searchString))
        {
            var searchResults = await _context.Movies
                .Where(s => s.Title != null && s.Title.ToLower().Contains(searchString.ToLower()))
                .ToListAsync();

            return View(searchResults);
        }
        else
        {
            var trendingMovies = await _context.Movies
                .OrderByDescending(m => m.Id)
                .Take(5)
                .ToListAsync();

            return View(trendingMovies);
        }
    }

    // GET: Movies/AllMovies
    [Authorize]
    public async Task<IActionResult> AllMovies(string? searchString)
    {
        ViewData["IsSearch"] = !string.IsNullOrWhiteSpace(searchString);

        var movies = from m in _context.Movies select m;

        if (!string.IsNullOrEmpty(searchString))
        {
            movies = movies.Where(s => s.Title != null && s.Title.ToLower().Contains(searchString.ToLower()));
        }

        return View(await movies.ToListAsync());
    }

    // GET: Movies/Details/5
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return BadRequest();

        var movie = await _context.Movies
            .Include(m => m.MovieCategories)
                .ThenInclude(mc => mc.Category)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null) return NotFound();

        // Get current user id
        var userId = _userManager.GetUserId(User);

        // Check if this movie is already favorited or saved by the current user
        ViewBag.IsFavorited = await _context.UserFavourites
            .AnyAsync(uf => uf.UserId == userId && uf.MovieId == id);

        ViewBag.IsSaved = await _context.UserSaves
            .AnyAsync(us => us.UserId == userId && us.MovieId == id);

        return View(movie);
    }

    // GET: Movies/Create
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var viewModel = new MovieFormViewModel();
        await PopulateCategoriesAsync(viewModel);
        return View(viewModel);
    }

    // POST: Movies/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(MovieFormViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var movie = new Movie
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                ReleaseDate = viewModel.ReleaseDate,
                Duration = viewModel.Duration,
                Rating = viewModel.Rating,
                PosterUrl = viewModel.PosterUrl,
                TrailerUrl = viewModel.TrailerUrl,
                IsActive = viewModel.IsActive
            };

            _context.Add(movie);
            await _context.SaveChangesAsync();

            var selectedCategoryIds = viewModel.AvailableCategories
                .Where(c => c.IsSelected)
                .Select(c => c.CategoryId)
                .ToList();

            foreach (var categoryId in selectedCategoryIds)
            {
                var movieCategory = new MovieCategory
                {
                    MovieId = movie.Id,
                    CategoryId = categoryId
                };
                _context.MovieCategories.Add(movieCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        await PopulateCategoriesAsync(viewModel);
        return View(viewModel);
    }

    // GET: Movies/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return BadRequest();

        var movie = await _context.Movies
            .Include(m => m.MovieCategories)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null) return NotFound();

        var viewModel = new MovieFormViewModel
        {
            Id = movie.Id,
            Title = movie.Title ?? string.Empty,
            Description = movie.Description,
            ReleaseDate = movie.ReleaseDate,
            Duration = movie.Duration,
            Rating = movie.Rating,
            PosterUrl = movie.PosterUrl,
            TrailerUrl = movie.TrailerUrl,
            IsActive = movie.IsActive
        };

        await PopulateCategoriesAsync(viewModel, movie);
        return View(viewModel);
    }

    // POST: Movies/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, MovieFormViewModel viewModel)
    {
        if (id != viewModel.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                var movie = await _context.Movies
                    .Include(m => m.MovieCategories)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (movie == null) return NotFound();

                movie.Title = viewModel.Title;
                movie.Description = viewModel.Description;
                movie.ReleaseDate = viewModel.ReleaseDate;
                movie.Duration = viewModel.Duration;
                movie.Rating = viewModel.Rating;
                movie.PosterUrl = viewModel.PosterUrl;
                movie.TrailerUrl = viewModel.TrailerUrl;
                movie.IsActive = viewModel.IsActive;

                _context.MovieCategories.RemoveRange(movie.MovieCategories);

                var selectedCategoryIds = viewModel.AvailableCategories
                    .Where(c => c.IsSelected)
                    .Select(c => c.CategoryId)
                    .ToList();

                foreach (var categoryId in selectedCategoryIds)
                {
                    movie.MovieCategories.Add(new MovieCategory
                    {
                        MovieId = movie.Id,
                        CategoryId = categoryId
                    });
                }

                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Movies.Any(e => e.Id == viewModel.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        return View(viewModel);
    }
    // ွGET: Movies/Delete
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movies
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie != null)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    // Helper Method (Categories loading)
    private async Task PopulateCategoriesAsync(MovieFormViewModel viewModel, Movie? movie = null)
    {
        var allCategories = await _context.Categories.ToListAsync();
        var assignedCategoryIds = new HashSet<int>();

        if (movie != null)
        {
            assignedCategoryIds = new HashSet<int>(movie.MovieCategories.Select(c => c.CategoryId));
        }

        viewModel.AvailableCategories = allCategories.Select(c => new CategoryCheckBoxViewModel
        {
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName ?? string.Empty,
            IsSelected = assignedCategoryIds.Contains(c.CategoryId)
        }).ToList();
    }
}