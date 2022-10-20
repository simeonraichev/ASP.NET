using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static TaskBoardFinal.Data.DataConstants.User;
namespace TaskBoardFinal.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(MaxUserFirstName)]
        public string FirstName { get; init; } = null!;

        [Required]
        [MaxLength(MaxUserLastName)]
        public string LastName { get; init; } = null!;

    }
}
