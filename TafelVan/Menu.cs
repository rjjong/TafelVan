using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TafelVan.Helpers;
using TafelVan.Modes;

namespace Tafel_van
{
    class Menu
    {
        static void Main(string[] args)
        {
            while (true)
            {
                UIHelpers.CreateNewTitle("Menu");

                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Welk tafelsoort wil je oefenen?")
                        .PageSize(4)
                        .AddChoices(new[] {
                            "1. Klassieke tafels oefenen  (a x b)",
                            "2. Duo tafels oefenen        (a x b + c x d)",
                            "3. Trio tafels oefenen       (a x b + c x d + e x f)",
                            "4. Afsluiten",
                        }));

                switch (option)
                {
                    case "1. Klassieke tafels oefenen  (a x b)":
                        Oefening.TafelVan("Klassieke");
                        break;
                    case "2. Duo tafels oefenen        (a x b + c x d)":
                        Oefening.TafelVan("Duo");
                        break;
                    case "3. Trio tafels oefenen       (a x b + c x d + e x f)":
                        Oefening.TafelVan("Trio");
                        break;
                    case "4. Afsluiten":
                        return;
                    default:
                        Console.WriteLine("Dit is geen optie. Druk op Enter om iets anders te kiezen.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
