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

                //AddCampersToCabin(context);

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
            CabinInfo,
            NewCabin
        };
        public static void DanielsMenu()
        {
            string[] menuChoices = new string[]
            {
                "Skriv in kampare",
                "Hämta ut kampare",
                "Se kampares lägerhus",
                "Se kampares gruppledare",
                "Se info om lägerhus",
                "Skapa ny stuga med lägerledare"
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
                if (choice == (int)Choices.NewCabin) CreateNewCabinAndCounselour();
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
                //======
                // Ask for Next of kin info
                string bigfirstname = MenuUtils.AskForString("Vad heter målsmannen i förnamn?");
                string biglastname = MenuUtils.AskForString("Vad heter målsmannen i efternamn?");
                // Query data from next of kin

                NextOfKin nextOfKin = (from nextOfKins in context.NextOfKins
                                       where nextOfKins.FirstName == bigfirstname
                                       && nextOfKins.LastName == biglastname
                                       select nextOfKins).FirstOrDefault();
                context.Entry(nextOfKin).Collection(c => c.Campers).Load();

                int campersCount = nextOfKin.Campers.Count();
                string[] campersFirstName = new string[campersCount];
                string[] campersLastName = new string[campersCount];
                int[] camperIds = new int[campersCount];
                int i = 0;
                foreach (var campersInQuery in nextOfKin.Campers)
                {
                    campersFirstName[i] = campersInQuery.FirstName;
                    campersLastName[i] = campersInQuery.LastName;
                    camperIds[i] = campersInQuery.CamperId;
                    i++;

                }
                // Checks out and prints all the campers of the next of kin.
                int choice = MenuUtils.AlternetivesMenu(0, campersFirstName, "Vilken unge vill du hämta?");

                //var camperQuery = (from camper in context.Campers
                //                   where camper.LastName == campersLastName[choice] &&
                //                   camper.FirstName == campersFirstName[choice]
                //                   select camper.Cabin).ToList();
                string camperFirstName = campersFirstName[choice];
                string camperLastName = campersLastName[choice];
                int camperId = camperIds[choice];
                //======


                string camperName = MenuUtils.AskForString("Vad heter parveln i förnamn?");
                var camperQuery = (from camper in context.Campers
                                  where camper.CamperId == camperId
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
                //======
                // Ask for Next of kin info
                string bigfirstname = MenuUtils.AskForString("Vad heter målsmannen i förnamn?");
                string biglastname = MenuUtils.AskForString("Vad heter målsmannen i efternamn?");
                // Query data from next of kin

                NextOfKin nextOfKin = (from nextOfKins in context.NextOfKins
                                       where nextOfKins.FirstName == bigfirstname
                                       && nextOfKins.LastName == biglastname
                                       select nextOfKins).FirstOrDefault();
                context.Entry(nextOfKin).Collection(c => c.Campers).Load();

                int campersCount = nextOfKin.Campers.Count();
                string[] campersFirstName = new string[campersCount];
                string[] campersLastName = new string[campersCount];
                int[] camperIds = new int[campersCount];
                int i = 0;
                foreach (var campersInQuery in nextOfKin.Campers)
                {
                    campersFirstName[i] = campersInQuery.FirstName;
                    campersLastName[i] = campersInQuery.LastName;
                    camperIds[i] = campersInQuery.CamperId;
                    i++;

                }
                // Checks out and prints all the campers of the next of kin.
                int choice = MenuUtils.AlternetivesMenu(0, campersFirstName, "Vilken unge vill du hämta?");

                //var camperQuery = (from camper in context.Campers
                //                   where camper.LastName == campersLastName[choice] &&
                //                   camper.FirstName == campersFirstName[choice]
                //                   select camper.Cabin).ToList();
                string camperFirstName = campersFirstName[choice];
                string camperLastName = campersLastName[choice];
                int camperId = camperIds[choice];
                //======

                string camperName = MenuUtils.AskForString("Vad heter parveln i förnamn?");
                var camperQuery = (from camper in context.Campers
                                  where camper.CamperId == camperId
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
                // Ask for Next of kin info
                string bigfirstname = MenuUtils.AskForString("Vad heter målsmannen i förnamn?");
                string biglastname = MenuUtils.AskForString("Vad heter målsmannen i efternamn?");
                // Query data from next of kin

                NextOfKin nextOfKin = (from nextOfKins in context.NextOfKins
                                         where nextOfKins.FirstName == bigfirstname
                                         && nextOfKins.LastName == biglastname
                                         select nextOfKins).FirstOrDefault();
                context.Entry(nextOfKin).Collection(c => c.Campers).Load();

                int campersCount = nextOfKin.Campers.Count();
                string[] campersFirstName = new string[campersCount];
                string[] campersLastName = new string[campersCount];
                int[] camperIds = new int[campersCount];
                int i = 0;
                foreach (var campersInQuery in nextOfKin.Campers)
                {
                    campersFirstName[i] = campersInQuery.FirstName;
                    campersLastName[i] = campersInQuery.LastName;
                    camperIds[i] = campersInQuery.CamperId;
                    i++;
                
                }
                // Checks out and prints all the campers of the next of kin.
                int choice = MenuUtils.AlternetivesMenu(0, campersFirstName, "Vilken unge vill du hämta?");

                //var camperQuery = (from camper in context.Campers
                //                   where camper.LastName == campersLastName[choice] &&
                //                   camper.FirstName == campersFirstName[choice]
                //                   select camper.Cabin).ToList();
                
                int camperId = camperIds[choice];
                context.Campers.RemoveRange(context.Campers.Where(c => c.CamperId == camperId));
                context.SaveChanges();

            }

        }

        private static bool ContainsNextOfKin(NextOfKin nextOfKin, List<NextOfKin> nextOfKins)
        {
            foreach (NextOfKin kin in nextOfKins)
            {
                if (kin.NextOfKinId == nextOfKin.NextOfKinId)
                {
                    return true;
                }
            }
            return false;
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
                // Let user choose cabin for all campers.
                bool chooseCabin = MenuUtils.AlternetivesMenu(1, new string[] { "Ja, nu på en gång.", "Nej, det kan göras senare." }, "Vill du välja stugor själv?") == 0 ? true : false;
                if (chooseCabin)
                {
                    foreach (var camper in nextOfKin.Campers)
                    {
                        var cabins = (from emptyCabins in context.Cabins
                                     where emptyCabins.Campers.Count <= 3
                                     select emptyCabins).ToList();
                        // Will query for all empty cabins
                        string[] cabinText = new string[cabins.Count()];
                        int y = 0;
                        foreach (var cabin in cabins)
                        {
                            context.Entry(cabin).Collection(c => c.Campers).Load();
                            cabinText[y] = String.Format($"{cabin.Name}: {3 - cabin.Campers.Count}");
                        }
                        // Let user choose from cabins, and see remaining spots.

                        int cabinChoice = MenuUtils.AlternetivesMenu(0, cabinText, "välj cabin för " + camper.FirstName + ".");
                        y++;
                        camper.Cabin = cabins[cabinChoice];
                        cabins[cabinChoice].Campers.Add(camper);


                        // Loop until all campers has a cabin.
                    }
                }
                context.SaveChanges();
            }
        }
        private static void CreateNewCabinAndCounselour()
        {
            using(CampSleepawayContext context = new CampSleepawayContext())
            {
                string bigfirstname = MenuUtils.AskForString("Vad heter Lägerledaren i förnamn?");
                string biglastname = MenuUtils.AskForString("Vad heter Lägerledaren i efternamn?");


                
                string cabinName = MenuUtils.AskForString("Vad ska kabinen heta?");
                Counselor counselor = new Counselor { FirstName = bigfirstname, LastName = biglastname };

                Cabin cabin = new Cabin { Name = cabinName, Counselor = counselor };

                context.Cabins.Add(cabin);

                context.SaveChanges();
            }
        }

        private static void AddOneCabin(CampSleepawayContext context)
        {
            Cabin cabin = new Cabin { Name = "Moonbase" };
            context.Cabins.Add(cabin);
            context.SaveChanges();
        }
        private static void AddOneCabinWithCampers(CampSleepawayContext context)
        {
            NextOfKin next = context.NextOfKins.Find(1);
            List<NextOfKin> nextOfKins = new List<NextOfKin>();
            nextOfKins.Add(next);

            List<Camper> campers = new List<Camper>
                {
                    new Camper { FirstName = "Karl", LastName = "Ekman", NextOfKins = nextOfKins  },
                    new Camper { FirstName = "Berit", LastName = "Ekman" ,NextOfKins = nextOfKins},
                    
                    new Camper { FirstName = "Lisa", LastName = "Ekdhal", NextOfKins = nextOfKins }
                };
            Cabin cabin = context.Cabins.Find(1);
            cabin.Counselor = context.Counselors.Find(2);
            context.Cabins.Add(cabin);
            context.SaveChanges();
        }
        
        private static void AddCampersToCabin(CampSleepawayContext context)
        {
            Cabin cabin = context.Cabins.Find(5);
            Camper camper = new Camper { FirstName = "Ante", LastName = "Berg" };
            context.Entry(cabin).Collection(c => c.Campers).Load();

            context.Campers.Add(camper);
            cabin.Campers.Add(camper);

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

            Counselor counselor = new Counselor { FirstName = "Gösta Ekman" };
            
            context.Counselors.Add(counselor);

            Cabin rock = context.Cabins.Find(1);

            rock.Counselor = counselor;
            //moonbase.Counselor = counselor;



            context.SaveChanges();

        }
    }
}