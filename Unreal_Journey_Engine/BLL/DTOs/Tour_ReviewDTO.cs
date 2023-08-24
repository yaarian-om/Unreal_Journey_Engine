using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class Tour_ReviewDTO
    {
        public int Review_ID { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        [StringLength(500, ErrorMessage = "Comment must be between 1 and 500 characters")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Tour ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Tour ID must be a positive integer")]
        public int Tour_ID { get; set; }

        [Required(ErrorMessage = "Tourist ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Tourist ID must be a positive integer")]
        public int Tourist_ID { get; set; }
    }
}
