using System;

namespace CamperSleepaway
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Välj\n 1. Daniel \n 2. Jonathan \n 3. Linus\n 4. Avsluta");
                int respons = Convert.ToInt32(Console.ReadLine());
                if (respons == 1) Daniel.Program.DanielTest();
                if (respons == 2) Jonathan.Program.JonathanTest();
                if (respons == 3) Linus.Program.LinusTest();
                if (respons == 4) running = false;

            }

        }
    }
}
