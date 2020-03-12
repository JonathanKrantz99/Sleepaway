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
        //public DbSet<Counselor> Counselors { get; set; }
        public DbSet<NextOfKin> NextOfKins { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Camper>()
            //    .Property(u => u.CamperId)
            //    .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema
            //    .DatabaseGeneratedOption.Identity);


            //modelBuilder.Entity<Cabin>()
            //    .HasOptional(c => c.Counselor)
            //    .WithRequired(co => co.Cabin);
            modelBuilder.Entity<Cabin>()

                .HasMany(c => c.Campers)
                .WithOptional(ca => ca.Cabin);






            //modelBuilder.Configurations.Add(new Class1Map());

            //modelBuilder.Entity<Country>()
            //.HasMany(c => c.Users)
            //.WithOptional(c => c.Country)
            //.HasForeignKey(c => c.CountryId)
            //.WillCascadeOnDelete(false);
        }


    }
}
