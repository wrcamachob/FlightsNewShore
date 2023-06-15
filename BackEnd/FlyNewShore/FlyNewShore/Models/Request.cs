using System.ComponentModel.DataAnnotations;

namespace FlyNewShore.Models
{
    public class Request
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "The field origin must be alphanumeric")]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string Origin { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "The field destination must be alphanumeric")]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string Destination { get; set; }
    }
}
