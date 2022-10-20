using System.ComponentModel.DataAnnotations;

namespace TaskBoardFinal.Data.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string Title  { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int BoardId { get; set; }

        public Board Board { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }
    }
}
