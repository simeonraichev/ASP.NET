namespace TaskBoardApp.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Task
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.TaskTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DataConstants.TaskDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int BoardId { get; set; }
        public Board Board { get; set; } = null!;

        [Required]
        public string OwnerId { get; set; } = null!;
        public User Owner { get; set; } = null!;
    }
}
