namespace TaskBoardApp.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using Data;

    public class User : IdentityUser
    {
        [Required]
        [MaxLength(DataConstants.UserMaxNamesLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(DataConstants.UserMaxNamesLength)]
        public string LastName { get; set; } = null!;

        public IList<Task> Tasks { get; set; } = new List<Task>();
    }
}
