
using System.ComponentModel.DataAnnotations;

namespace ASPInstruduction.Models
{
    public class TestModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage = "Oooooops")]
        public string Product { get; set; }
    }
}
