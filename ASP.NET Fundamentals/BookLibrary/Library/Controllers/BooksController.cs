using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Library.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookService bookService;
        private readonly LibraryDbContext context;

        public BooksController(IBookService _bookService, LibraryDbContext _context)
        {
            bookService = _bookService;
            context = _context;

        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            //var model = await movieService.GetAllAsync();
            //return View(model);
            var entities = await context.Books.Include(m => m.Category).ToListAsync();

            var model = entities
                .Select(m => new BookViewModel()
                {
                    Author = m.Author,
                    Category = m?.Category.Name,
                    Id = m.Id,
                    ImageUrl = m.ImageUrl,
                    Rating = m.Rating,
                    Title = m.Title
                });

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {

            var model = new AddBookViewModel()
            {
                Categories = await context.Categories.ToListAsync(),
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
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
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return View(model);
            }
        }

        public async Task<IActionResult> AddToCollection(int bookId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


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
                //if (!user.ApplicationUsersBooks.Any(m => m.BookId == bookId))
                //{
                //    user.ApplicationUsersBooks.Add(new ApplicationUserBook()
                //    {
                //        BookId = book.Id,
                //        ApplicationUserId= applicationUserId.Id,
                //        Book = book.Title,
                //        ApplicationUser = applicationUser
                //    });
                //}
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
                .Include(u => u.ApplicationUsersBooks)
                .ThenInclude(um => um.Book)
                .ThenInclude(m => m.Category)
                .FirstOrDefaultAsync(u => u.Id == userId);




            if (user == null)
            {
                throw new ArgumentException("Invalid user id");
            }
            var model = user.ApplicationUsersBooks
               .Select(m => new BookViewModel()
               {
                   Author = m.Book.Author,
                   Category = m.Book?.Category.Name,
                   Id = m.BookId,
                   ImageUrl = m.Book.ImageUrl,
                   Title = m.Book.Title,
                   Rating = m.Book.Rating
               });
            return View("Mine", model);

        }

        public async Task<IActionResult> RemoveFromCollection(int bookId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.ApplicationUsersBooks)
                .ThenInclude(um => um.Book)
                .ThenInclude(m => m.Category)
                .FirstOrDefaultAsync(u => u.Id == userId);
            return RedirectToAction(nameof(Watched));


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
