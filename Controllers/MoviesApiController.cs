using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCMovie.Models;

namespace MVCMovie.Controllers;

[Authorize] // Login ဝင်ထားသူများသာ သုံးခွင့်ရမည်
[ApiController]
[Route("api/movies")]
public class MoviesApiController : ControllerBase
{
    private readonly MovieContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public MoviesApiController(MovieContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // POST: api/movies/toggle-favourite
    [HttpPost("toggle-favourite")]
    public async Task<IActionResult> ToggleFavourite([FromBody] int movieId)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var favourite = await _context.UserFavourites
            .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.MovieId == movieId);

        bool isFavourite;

        if (favourite == null)
        {
            // မရှိသေးရင် Favourite ထဲထည့်မယ်
            _context.UserFavourites.Add(new UserFavourite { UserId = userId, MovieId = movieId });
            isFavourite = true;
        }
        else
        {
            // ရှိပြီးသားဆိုရင် Favourite ထဲက ပြန်ဖြုတ်မယ်
            _context.UserFavourites.Remove(favourite);
            isFavourite = false;
        }

        await _context.SaveChangesAsync();
        return Ok(new { success = true, isFavourite });
    }

    // POST: api/movies/toggle-save
    [HttpPost("toggle-save")]
    public async Task<IActionResult> ToggleSave([FromBody] int movieId)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var save = await _context.UserSaves
            .FirstOrDefaultAsync(us => us.UserId == userId && us.MovieId == movieId);

        bool isSaved;

        if (save == null)
        {
            // မရှိသေးရင် Save ထဲထည့်မယ်
            _context.UserSaves.Add(new UserSave { UserId = userId, MovieId = movieId });
            isSaved = true;
        }
        else
        {
            // ရှိပြီးသားဆိုရင် Save ထဲက ပြန်ဖြုတ်မယ်
            _context.UserSaves.Remove(save);
            isSaved = false;
        }

        await _context.SaveChangesAsync();
        return Ok(new { success = true, isSaved });
    }
}