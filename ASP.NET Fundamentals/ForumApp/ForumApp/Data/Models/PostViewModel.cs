using System.ComponentModel.DataAnnotations;

namespace ForumApp.Data.Models
{
    public class PostViewModel : AddPostViewModel
    {
        public int Id { get; set; }
    }
}
