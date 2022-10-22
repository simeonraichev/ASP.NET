using Library.Data.Models;
using Library.Models.Books;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAllAsync();

        Task<IEnumerable<Category>> GetCategorysAsync();

        Task AddBookAsync(AddBookViewModel viewModel);

        Task AddBookToCollectionAsync(int bookId, string userId);

        Task<IEnumerable<BookViewModel>> GetReadAsync(string userId);

        Task RemoveFromCollectionAsync(int bookId, string userId);
    }
}
