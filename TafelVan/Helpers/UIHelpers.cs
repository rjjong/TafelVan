using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TafelVan.Helpers
{
    internal class UIHelpers
    {
        public static void CreateNewTitle(string title)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("Tafel Generator")
                    .Centered()
                    .Color(Color.Teal));
            AnsiConsole.Write(new Rule($"[orange1]{title}[/]").LeftJustified());
        }
    }
}
