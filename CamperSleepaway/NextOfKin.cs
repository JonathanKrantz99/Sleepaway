using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CamperSleepaway
{
    public class NextOfKin
    {
        public int NextOfKinId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public List<Camper> Campers { get; set; }
    }
}
