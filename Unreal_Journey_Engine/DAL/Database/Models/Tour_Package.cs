using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Database.Models
{
    public class Tour_Package
    {

        [Key]
        public int Tour_ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        public string Cost { get; set; }

        [Required]
        public string Image { get; set; }



        #region Relationship

        /// <summary>
        /// One Tour Package Can have many Bookings
        /// One Tour Package Can have many Tour Reviews
        /// </summary>
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Tour_Review> Tour_Reviews { get; set; }

        public Tour_Package()
        {
            Bookings = new List<Booking>();
            Tour_Reviews = new List<Tour_Review>();
        }


        // Foreign Key
        [Required]
        public int Tour_Guide_ID { get; set; }
        
        #endregion Relationship
    }
}
