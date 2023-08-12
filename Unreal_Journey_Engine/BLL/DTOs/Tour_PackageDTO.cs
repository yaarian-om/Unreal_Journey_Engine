using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class Tour_PackageDTO
    {
        public int Tour_ID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [MaxLength(255, ErrorMessage = "Location cannot exceed 255 characters.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid duration format.")]
        public string Duration { get; set; }

        [Required(ErrorMessage = "Cost is required.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid cost format.")]
        public string Cost { get; set; }

        [Required(ErrorMessage = "Image Name is required.")]
        [MaxLength(255, ErrorMessage = "Image Name cannot exceed 255 characters.")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Tour Guide ID is required.")]
        public int Tour_Guide_ID { get; set; }


    }
}
