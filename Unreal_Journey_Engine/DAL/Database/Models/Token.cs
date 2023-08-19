using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Database.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string TokenKey { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiredAt { get; set; }


        [ForeignKey("User")]
        public int User_ID { get; set; }
        public virtual User User { get; set; }
    }
}
