    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MVCMovie.Models;

    namespace MVCMovie.Controllers
    {
        [Authorize]
        public class ProfileController : Controller
        {
            private readonly MovieContext _context;
            private readonly UserManager<ApplicationUser> _userManager;

            public ProfileController(MovieContext context, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

        // GET: Profile
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge();
            }

            return View(user);
        }

        // GET: Profile/Favourites
        public async Task<IActionResult> Favourites()
            {
                // Get current logged-in user id
                var userId = _userManager.GetUserId(User);

                // Fetch favourite movies for this user
                var favouriteMovies = await _context.UserFavourites
                    .Where(uf => uf.UserId == userId)
                    .Include(uf => uf.Movie)
                    .Select(uf => uf.Movie)
                    .ToListAsync();

                return View(favouriteMovies);
            }

            // GET: Profile/Saved
            public async Task<IActionResult> Saved()
            {
                // Get current logged-in user id
                var userId = _userManager.GetUserId(User);

                // Fetch saved movies for this user
                var savedMovies = await _context.UserSaves
                    .Where(us => us.UserId == userId)
                    .Include(us => us.Movie)
                    .Select(us => us.Movie)
                    .ToListAsync();

                return View(savedMovies);
            }

            // POST: Profile/AddToFavourite
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AddToFavourite(int movieId)
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    return Challenge();
                }

                // Check if already in favourites
                var alreadyFavourite = await _context.UserFavourites
                    .AnyAsync(uf => uf.UserId == userId && uf.MovieId == movieId);

                if (!alreadyFavourite)
                {
                    // Add to favourites
                    var fav = new UserFavourite
                    {
                        UserId = userId,
                        MovieId = movieId
                    };
                    _context.UserFavourites.Add(fav);
                }
                else
                {
                    // Remove from favourites if already exists
                    var existing = await _context.UserFavourites
                        .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.MovieId == movieId);

                    if (existing != null)
                    {
                        _context.UserFavourites.Remove(existing);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Movies", new { id = movieId });
            }

            // POST: Profile/AddToSave
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AddToSave(int movieId)
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    return Challenge();
                }

                // Check if already saved
                var alreadySaved = await _context.UserSaves
                    .AnyAsync(us => us.UserId == userId && us.MovieId == movieId);

                if (!alreadySaved)
                {
                    // Add to saved list
                    var save = new UserSave
                    {
                        UserId = userId,
                        MovieId = movieId
                    };
                    _context.UserSaves.Add(save);
                }
                else
                {
                    // Remove from saved list if already exists
                    var existing = await _context.UserSaves
                        .FirstOrDefaultAsync(us => us.UserId == userId && us.MovieId == movieId);

                    if (existing != null)
                    {
                        _context.UserSaves.Remove(existing);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Movies", new { id = movieId });
            }
        }
    }