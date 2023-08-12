using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Database.Models
{
    public class Feedback
    {

        [Key]
        public int Feedback_ID { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }


        #region Relationship
        /// <summary>
        /// One User Can have Many Tour Feedbacks
        /// </summary>
        [ForeignKey("User")]
        // Here int? is for Null Allowed
        public int User_ID { get; set; }
        public virtual User User { get; set; }
        
        #endregion Relationship
    }
}
