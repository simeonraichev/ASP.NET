using System.ComponentModel.DataAnnotations;

namespace ForumApp.Data.Models
{
    public class PostViewModel
    {
        [UIHint("hidden")]
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Field {0} is required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Field must contain between {2} and {1} simbols.")]
        public string Title { get; set; } = null!;

        [Display(Name = "Content")]
        [Required(ErrorMessage = "Field {0} is required")]
        [StringLength(1500, MinimumLength = 30, ErrorMessage = "Field must contain between {2} and {1} simbols.")]
        public string Content { get; set; } = null!;
    }
}
