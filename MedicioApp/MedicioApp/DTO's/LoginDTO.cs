using System.ComponentModel.DataAnnotations;

namespace MedicioApp.DTO_s
{
    public class LoginDTO
    {
        [Required]
        [MinLength(7)]
        [MaxLength(30)]
        public string UsernameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
