using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TafelVan.Helpers;
using TafelVan.Models;

namespace TafelVan.Modes
{
    internal class Oefening
    {
        public static void TafelVan(string mode)
        {
            UIHelpers.CreateNewTitle("Settings");

            //Genereer tafels
            var tafels = AnsiConsole.Prompt(
                new MultiSelectionPrompt<int>()
                    .Title("Selecteer de [teal]tafels[/] die je wil oefenen?")
                    .PageSize(25).WrapAround(true).Required()
                    .MoreChoicesText("[grey](Beweeg omhoog of omlaag om meer te tonen)[/]")
                    .InstructionsText(
                        "[grey](Press [blue]<space>[/] to toggle a number, " +
                        "[green]<enter>[/] to accept)[/]")
                    .AddChoiceGroup(0, Choices.Group0to9)
                    .AddChoiceGroup(10, Choices.Group10to19)
                    .AddChoiceGroup(20, Choices.Group20to29)
                    .AddChoiceGroup(30, Choices.Group30to39)
                    .AddChoiceGroup(40, Choices.Group40to49)
                    .AddChoiceGroup(50, Choices.Group50to59)
                    .AddChoiceGroup(60, Choices.Group60to69)
                    .AddChoiceGroup(70, Choices.Group70to79)
                    .AddChoiceGroup(80, Choices.Group80to89)
                    .AddChoiceGroup(90, Choices.Group90to99)
                    .AddChoiceGroup(100, Choices.Group100to1000)
                    .Select(5).Select(8).Select(11).Select(17).Select(35));

            UIHelpers.CreateNewTitle("Settings");

            //Krijg overige instellingen
            var tafelMin = AnsiConsole.Ask<int>("Wat is het minimale bereik dat je wilt oefenen?", 1);
            var tafelMax = AnsiConsole.Ask<int>("Wat is het maximale bereik dat je wilt oefenen?", 20);
            var somAantal = AnsiConsole.Ask<int>("Hoeveel oefensommen wil je creëren?", 48);

            //Maak samenvatting
            AnsiConsole.Write(new Rule("[orange1]Samenvatting[/]").LeftJustified());
            AnsiConsole.MarkupLine("Je hebt [red]{0}[/] tafels geselecteed, met een bereik tussen [red]{1}[/] en [red]{2}[/].", tafels.Count, tafelMin, tafelMax);
            AnsiConsole.MarkupLine("Er worden [red]{0}[/] sommen gegenereerd in [red]{1}[/] modus.", somAantal, mode);
            
            //Genereer sommen
            List<string> oefenSommen = new List<string>();
            oefenSommen = SomHelpers.CreëerSommen(mode, tafels, tafelMin, tafelMax, somAantal, out List<int> antwoorden);
            AnsiConsole.MarkupLine("\nTafels zijn gegenereerd. Ben je er klaar voor? (druk op [blue]Enter[/] om te beginnen)");
            Console.ReadLine();

            List<int> userAwnsers = SomHelpers.GetUserAwnsers(oefenSommen, out TimeSpan totalTime, out List<TimeSpan> awnserTime);
            List<string> results = SomHelpers.CheckResults(oefenSommen, userAwnsers, antwoorden, awnserTime, out int score);
            AnsiConsole.WriteLine("Oefening voltooid. Druk op Enter om resultaten te bekijken...");
            Console.ReadLine();

            UIHelpers.CreateNewTitle("Resultaten");

            foreach (string result in results)
                AnsiConsole.MarkupLine(result);

            AnsiConsole.WriteLine("");
            AnsiConsole.Write(new Rule("[orange1]Samenvatting[/]").LeftJustified());
            AnsiConsole.MarkupLine($"Score: [blue]{score * 100 / oefenSommen.Count}%[/]");
            AnsiConsole.MarkupLine($"Totale tijd: [blue]{totalTime}[/]");
            AnsiConsole.MarkupLine($"Gemiddelde tijd per vraag: [blue]{new TimeSpan(Convert.ToInt64(awnserTime.Average(t => t.Ticks)))}[/]");

            AnsiConsole.WriteLine("\nOefentoets beëindigd. Druk op Enter om te stoppen.");
            Console.ReadLine();
        }
    }
}
