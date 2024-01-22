using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BUSINESS.DTOs.AppUser
{
    public class AppUserDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]

        public string Username { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(25)]

        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(25)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string RepeatPassword { get; set; }
    }
}
