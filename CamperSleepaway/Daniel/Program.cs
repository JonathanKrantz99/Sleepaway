using System;
using System.Collections.Generic;
using System.Text;

namespace CamperSleepaway.Daniel
{
    public partial class Program
    {
        public static void DanielTest()
        {
            //Ska göra lite niceiga tester.

            using (CampSleepawayContext context = new CampSleepawayContext())
            {

                AddOneCabinWithCampers(context);
                //Console.WriteLine($"title {cabin.Name}, id: {cabin.CabinId}");

            }
        }

        private static void AddOneCabin(CampSleepawayContext context)
        {
            Cabin cabin = new Cabin { Name = "Area 51" };
            context.Cabins.Add(cabin);
            context.SaveChanges();
        }
        private static void AddOneCabinWithCampers(CampSleepawayContext context)
        {

            List<Camper> campers = new List<Camper>
                {
                    new Camper { FirstName = "Daniel", LastName = "Anderson" },
                    new Camper { FirstName = "Jonathan", LastName = "Krantz" },
                    new Camper { FirstName = "Riche", LastName = "Rich" }
                };
            Cabin cabin = new Cabin { Name = "Area 51", Campers = campers };
            context.Cabins.Add(cabin);
            context.SaveChanges();
        }

        private static void AddOneNextOfKin(CampSleepawayContext context)
        {
            NextOfKin nextOfKin = new NextOfKin { FirstName = "d" };

            context.NextOfKins.Add(nextOfKin);
            context.SaveChanges();

        }

        private static void AddOneCamper(CampSleepawayContext context)
        {
            context.Campers.Add(new Camper { FirstName = "Linus", LastName = "Örn" });
            context.SaveChanges();

        }
    }
}