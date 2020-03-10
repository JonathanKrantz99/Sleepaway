using System;

namespace CamperSleepaway
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            while (true){
                Console.WriteLine("Välj 1. Daniel \n 2. Jonathan \n 3. Linus");
                string respons = Console.ReadLine();
                if (respons == 1) Jonathan.Program.JonathanTest();
                if (respons == 2) Linus.Program.LinusTest();
                if (respons == 3) Daniel.Program.DanielTest();
            }
            
        }
    }
}
