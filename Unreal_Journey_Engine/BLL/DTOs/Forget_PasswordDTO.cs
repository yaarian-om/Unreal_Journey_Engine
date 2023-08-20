using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class Forget_PasswordDTO
    {

        [RegularExpression(@"^[\w\.-]+@[\w\.-]+\.\w+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [StringLength(15, MinimumLength = 8)]
        public string Password { get; set; }

        public string Pin { get; set; }


    }
}
