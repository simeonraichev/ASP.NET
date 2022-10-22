using System.ComponentModel.DataAnnotations;

namespace TaskBoardFinal.Models
{
    public class LoginViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
