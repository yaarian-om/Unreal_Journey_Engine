using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Database.Models
{
    public class Promotion_Table
    {
        [Key]
        public int Promotion_ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Discription { get; set; }

        [Required]
        public string Amount { get; set; }

    }
}
