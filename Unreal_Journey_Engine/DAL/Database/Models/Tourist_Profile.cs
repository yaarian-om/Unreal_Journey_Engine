using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Database.Models
{
    internal class Tourist_Profile
    {

        [Key]
        public int Tourist_ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Image { get; set; }



        #region Relationships
        
        /// <summary>
        /// One User (Tourist) Can have only one Profile
        /// </summary>
        [ForeignKey("User")]
        // Here int? is for Null Allowed
        public int User_ID { get; set; }
        public virtual User User { get; set; }


        /// <summary>
        /// One Tourist Can have many Bookings
        /// One Tourist Can have many Tour Reviews
        /// </summary>
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Tour_Review> Tour_Reviews { get; set; }


        public Tourist_Profile()
        {
            Bookings = new List<Booking>();
            Tour_Reviews = new List<Tour_Review>();
        }

        #endregion Relationships

    }
}
