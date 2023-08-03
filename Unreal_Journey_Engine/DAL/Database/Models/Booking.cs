using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Database.Models
{
    internal class Booking
    {

        [Key]
        public int Booking_ID { get; set; }

        [Required]
        public string Tour_Status { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Payment_Status { get; set; }



        #region Relationships
        

        [ForeignKey("Tourist_Profile")]
        // Here int? is for Null Allowed
        public int Tourist_ID { get; set; }
        public virtual Tourist_Profile Tourist_Profile { get; set; }

        [ForeignKey("Tour_Package")]
        // Here int? is for Null Allowed
        public int Tour_ID { get; set; }
        public virtual Tour_Package Tour_Package { get; set; }

        #endregion Relationships
    }
}
