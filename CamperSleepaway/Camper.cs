using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CamperSleepaway
{
    public class Camper
    {
        public int CamperId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public List<NextOfKin> NextOfKins { get; set; }

        //[ForeignKey("Cabin")]
        //public int CabinId { get; set; }
        public Cabin Cabin { get; set; }
    }
}
