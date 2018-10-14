using System.ComponentModel.DataAnnotations;

namespace NetBaires.Models
{
    public class ContactUsViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}