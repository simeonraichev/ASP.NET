using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models.Movies;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext context;

        public BookService(LibraryDbContext _context)
        {
            context = _context;
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            var entity = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                Rating = model.Rating,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,

            };

            await context.Books.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task AddBookToCollectionAsync(int bookId, string userId)
        {
            var user = await context.Users
              .Where(u => u.Id == userId)
              .Include(u => u.ApplicationUserBooks)
              .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var book = await context.Books.FirstOrDefaultAsync(u => u.Id == bookId);

            if (book == null)
            {
                throw new ArgumentException("Invalid Movie ID");
            }

            if (!user.ApplicationUserBooks.Any(m => m.BookId == bookId))
            {
                user.ApplicationUserBooks.Add(new ApplicationUserBook
                {
                    BookId = book.Id,
                    ApplicationUserId = user.Id,
                    Book = book,
                    ApplicationUser = user
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BookViewModel>> GetAllAsync()
        {
            var entities = await context.Books
                .Include(m => m.Category)
                .ToListAsync();


            return entities
               .Select(m => new BookViewModel()
               {
                   Author = m.Author,
                   Category = m?.Category?.Name,
                   Id = m.Id,
                   ImageUrl = m.ImageUrl,
                   Rating = m.Rating,
                   Title = m.Title,
                   Description = m.Description
                   
               });
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<BookViewModel>> GetWatchedAsync(string userId)
        {
            var user = await context.Users
              .Where(u => u.Id == userId)
              .Include(u => u.ApplicationUserBooks)
              .ThenInclude(um => um.Book)
              .ThenInclude(m => m.Category)
              .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.ApplicationUserBooks
                .Select(m => new BookViewModel()
                {
                    Author = m.Book.Author,
                    Category = m.Book.Category?.Name,
                    Id = m.BookId,
                    ImageUrl = m.Book.ImageUrl,
                    Title = m.Book.Title,
                    Rating = m.Book.Rating,
                });
        }

        public async Task RemoveBookFromCollectionAsync(int bookid, string userId)
        {
            var user = await context.Users
               .Where(u => u.Id == userId)
               .Include(u => u.ApplicationUserBooks)
               .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var movie = user.ApplicationUserBooks.FirstOrDefault(m => m.BookId == bookid);

            if (movie != null)
            {
                user.ApplicationUserBooks.Remove(movie);

                await context.SaveChangesAsync();
            }
        }
    }
}
