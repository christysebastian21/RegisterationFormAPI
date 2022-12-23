using System.ComponentModel.DataAnnotations;

namespace RegisterationForm.Models
{
    public class Registerations
    {
        public int RegisterationsID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
