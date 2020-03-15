using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CamperSleepaway
{
    public class Cabin
    {
        public int CabinId { get; set; }
        public string Name { get; set; }
        [MaxLength(3)]
        public List<Camper> Campers { get; set; }
        public Counselor Counselor { get; set; }
    }
}
