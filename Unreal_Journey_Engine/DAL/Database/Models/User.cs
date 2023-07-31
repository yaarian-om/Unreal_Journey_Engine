using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Database.Models
{
    internal class User
    {

        [Key]
        public int User_ID { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(255)] // Adjust the length as needed
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

    }
}
