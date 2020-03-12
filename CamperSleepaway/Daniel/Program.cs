using MenuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
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
                //AddOneCouncelour(context);

                //AddOneCabin(context);
                //AddOneCabinWithCampers(context);
                //AddOneNextOfKin(context);
                //AddOneCamper(context);
                //AddOneNextOfKinToCamper(context);
                //AddOneCamper(context);
                DanielsMenu();

                
                //Console.WriteLine($"title {cabin.Name}, id: {cabin.CabinId}");

            }
        }

        enum Choices { 
            CheckIn, 
            CheckOut, 
            CheckCampersCabin, 
            CheckCampersCouncelour, 
            CabinInfo 
        };
        public static void DanielsMenu()
        {
            string[] menuChoices = new string[]
            {
                "Skriv in kampare",
                "Hämta ut kampare",
                "Se kampares lägerhus",
                "Se kampares gruppledare",
                "Se info om lägerhus"
            };
            bool running = true;
            while (running)
            {
                int choice = MenuLibrary.MenuUtils.AlternetivesMenu(0, menuChoices, "Välj något");
                if (choice == (int)Choices.CheckIn) CheckInCamper();
                if (choice == (int)Choices.CheckOut) CheckOutCamper();
                if (choice == (int)Choices.CheckCampersCouncelour) CheckCampersCouncelour();
                if (choice == (int)Choices.CheckCampersCabin) CheckCampersCabin();
                if (choice == (int)Choices.CabinInfo) CabinInfo();
            }


        }

        private static void CabinInfo()
        {
            // hitta alla stugor in i en array
            using (CampSleepawayContext context = new CampSleepawayContext())
            {
                var cabinQuery = (from cabins in context.Cabins
                                  select cabins).ToList();
                string[] cabinNames = new string[cabinQuery.Count];
                for (int i = 0; i < cabinQuery.Count; i++)
                {
                    cabinNames[i] = cabinQuery[i].Name;
                }
                // visa lista över alla stugor
                int choice = MenuLibrary.MenuUtils.AlternetivesMenu(
                    0,
                    cabinNames,
                    "Välj vilken kabin du vill se info om");
                // visa info om valda kabin
                Cabin cabinInfo = cabinQuery[choice];
                context.Entry(cabinInfo).Reference(c => c.Counselor).Load();
                context.Entry(cabinInfo).Collection(c => c.Campers).Load();

                Console.WriteLine($"\nLägerhusets namn: {cabinInfo.Name}");
                    
                if (cabinInfo.Counselor != null)
                {
                    Console.WriteLine($"\nGruppledarens namn: {cabinInfo.Counselor.FirstName}");
                }
                if (cabinInfo.Campers != null)
                {
                    PrintCamperInfo(context, cabinInfo);
                }
            }
            MenuUtils.PauseUntilFeedback("Tryck en knapp för att fortsätta");

        }

        private static void PrintCamperInfo(CampSleepawayContext context, Cabin cabinInfo)
        {
            foreach (Camper camper in cabinInfo.Campers)
            {
                Console.WriteLine($"\nFörnamn: {camper.FirstName}\nEfternamn: {camper.LastName}\n");

                context.Entry(camper).Collection(c => c.NextOfKins).Load();
                if (camper.NextOfKins != null)
                {
                    Console.WriteLine("\nNext of kin:");
                    int kinCount = 1;
                    foreach (NextOfKin nextOfKin in camper.NextOfKins)
                    {
                        Console.WriteLine($"\n  ({kinCount})Next of kin:\n  {nextOfKin}");
                        kinCount++;
                    }
                }
            }
        }

        private static void CheckCampersCabin()
        {
            using (CampSleepawayContext context = new CampSleepawayContext())
            {
                string camperName = MenuUtils.AskForString("Vad heter parveln i förnamn?");
                var camperQuery = (from camper in context.Campers
                                  where camper.FirstName == camperName
                                  select camper.Cabin).ToList();
                

                // visa info om valda kabin
                Cabin cabinInfo = camperQuery[0];
                context.Entry(cabinInfo).Reference(c => c.Counselor).Load();
                context.Entry(cabinInfo).Collection(c => c.Campers).Load();
                if (cabinInfo != null)
                {
                    Console.WriteLine($"\nLägerhusets namn: {cabinInfo.Name}");

                    if (cabinInfo.Counselor != null)
                    {
                        Console.WriteLine($"\nGruppledarens namn: {cabinInfo.Counselor.FirstName}");
                    }
                    if (cabinInfo.Campers != null)
                    {
                        PrintCamperInfo(context, cabinInfo);
                    }
                }
                else
                {
                    Console.WriteLine("Hittade inget lägerhus till den kamparen");
                }
            }
            MenuUtils.PauseUntilFeedback("Tryck en knapp för att fortsätta");

        }

        private static void CheckCampersCouncelour()
        {
            using (CampSleepawayContext context = new CampSleepawayContext())
            {
                string camperName = MenuUtils.AskForString("Vad heter parveln i förnamn?");
                var camperQuery = (from camper in context.Campers
                                  where camper.FirstName == camperName
                                  select camper.Cabin).ToList();
                

                // visa info om valda kabin
                Cabin cabinInfo = camperQuery[0];
                context.Entry(cabinInfo).Reference(c => c.Counselor).Load();
                if (cabinInfo.Counselor != null)
                {
                    Console.WriteLine($"\nGruppledarens namn: {cabinInfo.Counselor.FirstName}");
                }
                else
                {
                    Console.WriteLine("Hittade ingen råggivare till den kamparen");
                }
            }
            MenuUtils.PauseUntilFeedback("Tryck en knapp för att fortsätta");

        }

        private static void CheckOutCamper()
        {
            using (CampSleepawayContext context = new CampSleepawayContext())
            {

                string firstname = MenuUtils.AskForString("Vad heter kamparen i förnamn?");
                string lastname = MenuUtils.AskForString("Vad heter kamparen i efternamn?");
                var camperQuery = (from camper in context.Campers
                                   where camper.FirstName == firstname &&
                                   camper.FirstName == lastname
                                   select camper.Cabin).ToList();
                context.Campers.RemoveRange(context.Campers.Where(c => c.FirstName == firstname && c.LastName == lastname));

            }

        }

        private static void CheckInCamper()
        {
            using(CampSleepawayContext context = new CampSleepawayContext())
            {
                string bigfirstname = MenuUtils.AskForString("Vad heter målsmannen i förnamn?");
                string biglastname = MenuUtils.AskForString("Vad heter målsmannen i efternamn?");


                bool oneMore = true;
                List<Camper> family = new List<Camper>();
                while (oneMore)
                {
                    string firstname = MenuUtils.AskForString("Vad heter kamparen i förnamn?");
                    string lastname = MenuUtils.AskForString("Vad heter kamparen i efternamn?");

                    family.Add(new Camper { FirstName = firstname, LastName = lastname});

                    oneMore = MenuUtils.AlternetivesMenu(1, new string[] { "Ja", "Nej" }, "Var det alla?") == 0 ? false : true;
                }

                NextOfKin nextOfKin = new NextOfKin
                {
                    FirstName = bigfirstname,
                    LastName = biglastname,
                    Campers = family
                };
                context.NextOfKins.Add(nextOfKin);
                context.SaveChanges();
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
            NextOfKin nextOfKin = new NextOfKin { FirstName = "Bro" };

            context.NextOfKins.Add(nextOfKin);
            context.SaveChanges();

        }

        private static void AddOneNextOfKinToCamper(CampSleepawayContext context)
        {
            NextOfKin nextOfKin = new NextOfKin
            {
                FirstName = "Päron till farsa",
                Campers = new List<Camper> { new Camper { FirstName = "Åberg" }, new Camper { FirstName = "Lus" } }
            };


            context.NextOfKins.Add(nextOfKin);
            context.SaveChanges();

        }

        private static void AddOneCamper(CampSleepawayContext context)
        {
            context.Campers.Add(new Camper { FirstName = "Anton", LastName = "Arnberg" });
            context.SaveChanges();

        }


        private static void AddOneCouncelour(CampSleepawayContext context)
        {

            Counselor counselor = new Counselor { FirstName = "Paul" };
            
            context.Counselors.Add(counselor);



            context.SaveChanges();

        }
    }
}