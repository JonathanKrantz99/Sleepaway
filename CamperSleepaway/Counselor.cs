using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CamperSleepaway
{
    public class Counselor
    {
        [Key]
        public int CounselorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //[ForeignKey("Cabin")]
        //public int CabinId { get; set; }
        public virtual Cabin Cabin { get; set; }
    }
}
