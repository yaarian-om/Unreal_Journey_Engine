using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Database.Models
{
    internal class Pin_Code
    {
        [Key]
        public int Pin_Code_ID { get; set; }

        public int Pin { get; set; }


        [ForeignKey("User")]
        public int User_ID { get; set; }
        public virtual User User { get; set; }
    }
}
