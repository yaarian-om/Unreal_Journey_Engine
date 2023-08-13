using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class Promotion_TableDTO
    {
        public int Promotion_ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Discription { get; set; }

        [Required]
        public string Amount { get; set; }
    }
}
