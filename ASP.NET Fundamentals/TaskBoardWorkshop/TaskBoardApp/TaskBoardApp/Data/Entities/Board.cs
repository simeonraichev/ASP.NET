namespace TaskBoardApp.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Board
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.BoardNameMaxLength)]
        public string Name { get; set; } = null!;

        public IList<Task> Tasks { get; set; } = new List<Task>();
    }
}