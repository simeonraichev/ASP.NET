using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models.Books;
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

        public async Task AddBookToCollectionAsync(int bookId, string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.ApplicationUsersBooks)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user id");
            }


            var book = await context.Books.FirstOrDefaultAsync(u => u.Id == bookId);
            if (book == null)
            {
                throw new ArgumentException("Invalid movie id");
            }
            if (!user.ApplicationUsersBooks.Any(m => m.BookId == bookId))
            {
                user.ApplicationUsersBooks.Add(new ApplicationUserBook()
                {
                    BookId = book.Id,
                    ApplicationUserId = user.Id,
                    Book = book,
                    ApplicationUser = user
                });
            }
            await context.SaveChangesAsync();
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            var entity = new Book()
            {
                Author = model.Author,
                CategoryId = model.CategoryId,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                Title = model.Title
            };
            await context.Books.AddAsync(entity);
            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<BookViewModel>> GetAllAsync()
        {
            var entities = await context.Books.Include(m => m.Category).ToListAsync();

            return entities
                 .Select(m => new BookViewModel()
                 {
                     Author = m.Author,
                     Category = m?.Category.Name,
                     Id = m.Id,
                     ImageUrl = m.ImageUrl,
                     Rating = m.Rating,
                     Title = m.Title
                 });
        }

        public async Task<IEnumerable<Category>> GetCategorysAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<BookViewModel>> GetReadAsync(string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.ApplicationUsersBooks)
                .ThenInclude(um => um.Book)
                .ThenInclude(m => m.Category)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user id");
            }
            return user.ApplicationUsersBooks
                .Select(m => new BookViewModel()
                {
                    Author = m.Book.Author,
                    Category = m.Book?.Category.Name,
                    Id = m.BookId,
                    ImageUrl = m.Book.ImageUrl,
                    Title = m.Book.Title,
                    Rating = m.Book.Rating
                });
        }

        public async Task RemoveFromCollectionAsync(int bookId, string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.ApplicationUsersBooks)
                .ThenInclude(um => um.Book)
                .ThenInclude(m => m.Category)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user id");
            }
            var movie = user.ApplicationUsersBooks.FirstOrDefault(m => m.BookId == bookId);
            if (movie != null)
            {
                user.ApplicationUsersBooks.Remove(movie);

                await context.SaveChangesAsync();
            }
        }

    }
}
