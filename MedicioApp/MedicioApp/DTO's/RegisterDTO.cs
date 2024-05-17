using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace MedicioApp.DTO_s
{
    public class RegisterDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Firstname { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Lastname { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(40)]
        public string Username { get; set; }
        [Required]
        [MinLength(7)]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MinLength(7)]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password),Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
