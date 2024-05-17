using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Doctor:BaseEntity
    {
        [Required]
        [MinLength(7)]
        [MaxLength(50)]
        public string FullName { get; set; } = null!;
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Position { get; set; } = null!;
        public string? ImgUrl { get; set; } = null!;
        [NotMapped]
        public IFormFile? PhotoFile { get; set; }

    }
}
