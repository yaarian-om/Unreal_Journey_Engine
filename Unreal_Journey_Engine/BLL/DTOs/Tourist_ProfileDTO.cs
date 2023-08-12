using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static AutoMapper.Internal.ExpressionFactory;
using System.Xml.Linq;

namespace BLL.DTOs
{
    internal class Tourist_ProfileDTO
    {

        public int Tourist_ID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Phone number must be 11 digits.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Image Name is required.")]
        [MaxLength(255, ErrorMessage = "Image Name cannot exceed 255 characters.")]
        public string Image { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int User_ID { get; set; }
    }

}
