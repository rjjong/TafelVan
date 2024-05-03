using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TafelVan.Helpers
{
    internal class Converters
    {
        public static int ConsoleReadInt()
        {
            int result;
            string? input = Console.ReadLine();

            while (!Int32.TryParse(input, out result))
            {
                Console.WriteLine("Not a valid number, try again.");

                input = Console.ReadLine();
            }

            return result;
        }
    }
}
