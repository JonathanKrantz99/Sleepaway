using System;
using System.Collections.Generic;
using System.Text;

namespace CamperSleepaway
{
    public class NextOfKin
    {
        public int NextOfKinId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Camper> Campers { get; set; }
    }
}
