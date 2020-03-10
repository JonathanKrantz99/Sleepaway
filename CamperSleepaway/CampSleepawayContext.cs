using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace CamperSleepaway
{
    public partial class CampSleepawayContext : DbContext
    {
        public DbSet<Camper> Campers { get; set; }
        public DbSet<Cabin> Cabins { get; set; }
        public DbSet<Counselor> Counselors { get; set; }
        public DbSet<NextOfKin> NextOfKins { get; set; }
    }
}
