using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class BookingDTO
    {
        public int Booking_ID { get; set; }

        [Required(ErrorMessage = "Tour Status is required")]
        [StringLength(50, ErrorMessage = "Tour Status cannot exceed 50 characters")]
        public string Tour_Status { get; set; }

        [Required(ErrorMessage = "Tour Starting Date is required")]
        [Display(Name = "Booking Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Payment Status is required")]
        [RegularExpression("^(Pending|Paid|Cancelled)$", ErrorMessage = "Invalid Payment Status")]
        public string Payment_Status { get; set; }

        [Required(ErrorMessage = "Tourist ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Tourist ID must be a positive value")]
        public int Tourist_ID { get; set; }

        [Required(ErrorMessage = "Tour ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Tour ID must be a positive value")]
        public int Tour_ID { get; set; }


    }
}
