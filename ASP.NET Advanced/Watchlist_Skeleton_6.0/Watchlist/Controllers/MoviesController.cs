using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Watchlist.Contracts;
using Watchlist.Data;
using Watchlist.Data.Models;
using Watchlist.Filters;
using Watchlist.Models;

namespace Watchlist.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;
        private readonly WatchlistDbContext context;

        public MoviesController(IMovieService _movieService, WatchlistDbContext _context)
        {
            movieService = _movieService;
            context = _context;

        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            //var model = await movieService.GetAllAsync();
            //return View(model);
            var entities = await context.Movies.Include(m => m.Genre).ToListAsync();

             var model = entities
                 .Select(m => new MovieViewModel()
                 {
                     Director = m.Director,
                     Genre = m?.Genre.Name,
                     Id = m.Id,
                     ImageUrl = m.ImageUrl,
                     Rating = m.Rating,
                     Title = m.Title
                 });
           
            return View(model);
        }

        [HttpGet]
        [MyActionAttribute]
        public async Task<IActionResult> Add()
        {

            var model = new AddMovieViewModel()
            {
                Genres = await context.Genre.ToListAsync(),
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var entity = new Movie()
                {
                    Director = model.Director,
                    GenreId = model.GenreId,
                    ImageUrl = model.ImageUrl,
                    Rating = model.Rating,
                    Title = model.Title
                };
                await context.Movies.AddAsync(entity);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return View(model);
            }
        }

        public async Task<IActionResult> AddToCollection(int movieId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

               
                var user = await context.Users
               .Where(u => u.Id == userId)
               .Include(u => u.UsersMovies)
               .FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new ArgumentException("Invalid user id");
                }


                var movie = await context.Movies.FirstOrDefaultAsync(u => u.Id == movieId);
                if (movie == null)
                {
                    throw new ArgumentException("Invalid movie id");
                }
                if (!user.UsersMovies.Any(m => m.MovieId == movieId))
                {
                    user.UsersMovies.Add(new UserMovie()
                    {
                        MovieId = movie.Id,
                        UserId = user.Id,
                        Movie = movie,
                        User = user
                    });
                }
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            

            return RedirectToAction(nameof(All));
        }
        public async Task<IActionResult> Watched()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersMovies)
                .ThenInclude(um => um.Movie)
                .ThenInclude(m => m.Genre)
                .FirstOrDefaultAsync(u => u.Id == userId);
            

            

            if (user == null)
            {
                throw new ArgumentException("Invalid user id");
            }
             var model = user.UsersMovies
                .Select(m => new MovieViewModel()
                {
                    Director = m.Movie.Director,
                    Genre = m.Movie?.Genre.Name,
                    Id = m.MovieId,
                    ImageUrl = m.Movie.ImageUrl,
                    Title = m.Movie.Title,
                    Rating = m.Movie.Rating
                });
            return View("Mine", model);

        }

        public async Task<IActionResult> RemoveFromCollection(int movieId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersMovies)
                .ThenInclude(um => um.Movie)
                .ThenInclude(m => m.Genre)
                .FirstOrDefaultAsync(u => u.Id == userId);
            return RedirectToAction(nameof(Watched));
            

            if (user == null)
            {
                throw new ArgumentException("Invalid user id");
            }
            var movie = user.UsersMovies.FirstOrDefault(m => m.MovieId == movieId);
            if (movie != null)
            {
                user.UsersMovies.Remove(movie);

                await context.SaveChangesAsync();
            }
        }

    }
}
 