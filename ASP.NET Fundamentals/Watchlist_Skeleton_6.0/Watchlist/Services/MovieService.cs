using Microsoft.EntityFrameworkCore;
using Watchlist.Contracts;
using Watchlist.Data;
using Watchlist.Data.Models;
using Watchlist.Models;

namespace Watchlist.Services
{
    public class MovieService : IMovieService
    {
        private readonly WatchlistDbContext context;

        public MovieService(WatchlistDbContext _context)
        {
            context = _context;
        }

        public async Task AddMovieToCollectionAsync(int movieId, string userId)
        {
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

        public async Task AddMoviewAsync(AddMovieViewModel model)
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

        }

        public async Task<IEnumerable<MovieViewModel>> GetAllAsync()
        {
            var entities = await context.Movies.Include(m=> m.Genre).ToListAsync();

           return entities
                .Select(m => new MovieViewModel()
                {
                    Director = m.Director,
                    Genre = m?.Genre.Name,
                    Id = m.Id,
                    ImageUrl = m.ImageUrl,
                    Rating = m.Rating,
                    Title = m.Title
                });
        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return await context.Genre.ToListAsync();
        }

        public async Task<IEnumerable<MovieViewModel>> GetWatchedAsync(string userId)
        {
            var user = await context.Users
                .Where(u=> u.Id == userId)
                .Include(u => u.UsersMovies)
                .ThenInclude(um=> um.Movie)
                .ThenInclude(m=> m.Genre)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user id");
            }
            return user.UsersMovies
                .Select(m => new MovieViewModel()
                {
                    Director = m.Movie.Director,
                    Genre = m.Movie?.Genre.Name,
                    Id = m.MovieId,
                    ImageUrl = m.Movie.ImageUrl,
                    Title = m.Movie.Title,
                    Rating = m.Movie.Rating
                });
        }

        public async Task RemoveFromCollectionAsync(int movieId, string userId)
        {
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
            var movie = user.UsersMovies.FirstOrDefault(m => m.MovieId == movieId);
            if (movie != null)
            {
                user.UsersMovies.Remove(movie);

                await context.SaveChangesAsync();
            }
        }
    }
}
