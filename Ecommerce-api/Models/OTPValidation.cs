using System.ComponentModel.DataAnnotations;

namespace Ecommerce_api.Models
{
    public class OtpValidation
    {
        [Required]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Otp { get; set; }
        public bool Validate { get; set; }
        public DateTime Datecreated { get; set; } 
    }
}
