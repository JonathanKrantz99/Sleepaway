using System;
using System.Collections.Generic;
using System.Text;

namespace CamperSleepaway
{
    public class Cabin
    {
        public int CabinId { get; set; }
        public string Name { get; set; }
        public List<Camper> Campers { get; set; }
        public Counselor Counselor { get; set; }
    }
}
