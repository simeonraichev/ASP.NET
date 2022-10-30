using System.ComponentModel.DataAnnotations;

namespace Watchlist.Data.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        //[Required]
        public string Name { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name)) yield return new ValidationResult("Username cannot be empty");
        }
    }
}
