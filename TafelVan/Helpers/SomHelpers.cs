using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TafelVan.Helpers
{
    internal class SomHelpers
    {
        public static List<string> CreëerSommen(string mode, List<int> tafels, int tafelMin, int tafelMax, int somAantal, out List<int> antwoorden)
        {
            List<string> sommen = new List<string>();
            antwoorden = new List<int>();

            Random random = new Random();

            for (int i = 0; i < somAantal; i++)
            {
                int rr1 = random.Next(tafelMin, tafelMax + 1);
                int rr2 = random.Next(tafelMin, tafelMax + 1);
                int rr3 = random.Next(tafelMin, tafelMax + 1);
                int rt1 = random.Next(tafels.Count);
                int rt2 = random.Next(tafels.Count);
                int rt3 = random.Next(tafels.Count);

                switch (mode)
                {
                    case "Klassieke":
                        sommen.Add($"{tafels[rt1]} x {rr1} = ");
                        antwoorden.Add(tafels[rt1] * rr1);
                        break;
                    case "Duo":
                        sommen.Add($"{tafels[rt1]} x {rr1} + {tafels[rt2]} x {rr2} = ");
                        antwoorden.Add(tafels[rt1] * rr1 + tafels[rt2] * rr2);
                        break;
                    case "Trio":
                        sommen.Add($"{tafels[rt1]} x {rr1} + {tafels[rt2]} x {rr2} + {tafels[rt3]} x {rr3} = ");
                        antwoorden.Add(tafels[rt1] * rr1 + tafels[rt2] * rr2 + tafels[rt3] * rr3);
                        break;
                    default:
                        break;
                }
            }

            return sommen;
        }

        public static List<int> GetUserAwnsers(List<string> oefenSommen, out TimeSpan totalTime, out List<TimeSpan> awnserTime)
        {
            List<int> userAwnsers = new List<int>();
            awnserTime = new List<TimeSpan>();
            totalTime = new TimeSpan();

            Stopwatch sw1 = new Stopwatch();
            sw1.Start();

            foreach (var som in oefenSommen.Select((value, i) => new { i, value }))
            {
                UIHelpers.CreateNewTitle($"[orange1]Som {som.i + 1} van de {oefenSommen.Count}[/]");

                userAwnsers.Add(AnsiConsole.Ask<int>(som.value));
                awnserTime.Add(sw1.Elapsed);

                totalTime += sw1.Elapsed;
                sw1.Restart();
            }

            sw1.Stop();

            return userAwnsers;
        }

        public static List<string> CheckResults(List<string> oefenSommen, List<int> userAwnser, List<int> antwoorden, List<TimeSpan> awnserTime, out int score)
        {
            List<string> results = new List<string>();
            score = 0;

            foreach (var som in oefenSommen.Select((value, i) => new { i, value }))
            {
                string? result;

                if ((userAwnser[som.i] == antwoorden[som.i]))
                {
                    score++;
                    result = "[green]Goed[/]";
                }
                else
                    result = "[red]Fout[/]";

                results.Add($"{som.value}{userAwnser[som.i]} \t {result} \t {awnserTime[som.i]}");
            }

            return results;
        }
    }
}
