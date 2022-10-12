using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyFitnessPlanner.Models
{
    public class LoginViewModel : Controller
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;



        [UIHint("hidden")]
        public string? ReturnUrl { get; set; }
    }
}
