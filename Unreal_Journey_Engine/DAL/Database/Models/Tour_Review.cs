using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Database.Models
{
    internal class Tour_Review
    {

        [Key]
        public int Review_ID { get; set; }

        [Required]
        public string Comment { get; set; }


        #region Relationship
        /// <summary>
        /// One Tour Package Can have Many Tour Reviews
        /// One Tourist Can have Many Tour Reviews
        /// </summary>
        [ForeignKey("Tour_Package")]
        // Here int? is for Null Allowed
        public int Tour_ID { get; set; }
        public virtual Tour_Package Tour_Package { get; set; }

        [ForeignKey("Tourist_Profile")]
        // Here int? is for Null Allowed
        public int Tourist_ID { get; set; }
        public virtual Tourist_Profile Tourist_Profile { get; set; }

        #endregion Relationship


    }
}
