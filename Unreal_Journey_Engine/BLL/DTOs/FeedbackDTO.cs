using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class FeedbackDTO
    {
        
        public int Feedback_ID { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        [StringLength(100, ErrorMessage = "Subject cannot exceed 100 characters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("^(Open|Closed)$", ErrorMessage = "Invalid Status")]
        public string Status { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        /*We have taken Zero because this will be calculated by session Hopefully*/
        [Range(0, int.MaxValue, ErrorMessage = "User ID must be a positive value")]
        public int User_ID { get; set; }
    }
}
