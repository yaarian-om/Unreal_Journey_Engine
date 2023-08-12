using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BLL.DTOs
{
    public class UserDTO
    {

        public int User_ID { get; set; }

        [Required]
        [RegularExpression(@"^[\w\.-]+@[\w\.-]+\.\w+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 8)]
        public string Password { get; set; }

        #region Defining Custom Values for Role
        public enum UserRole
        {
            Admin,
            Tourist,
            Tour_Guide
        }
        #endregion Defining Custom Values for Role


        [Required]
        [EnumDataType(typeof(UserRole))]
        public string Role { get; set; }
    }
}
