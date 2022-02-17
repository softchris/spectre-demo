using Spectre.Console;
using System.Reflection;
string path = Directory.GetCurrentDirectory();
// Console.WriteLine("Running in path {0}", path);
string toolRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
string baseToolPath =  new Uri(toolRoot + "/../../../").LocalPath;
string templatePath = Path.Combine(baseToolPath, "templates");

// Console.WriteLine("Template path {0}", templatePath);

AnsiConsole.Write(
    new FigletText("Scaffold-demo")
        .LeftAligned()
        .Color(Color.Red));

Console.WriteLine("");
var answerReadme = AnsiConsole.Confirm("Generate a [green]README[/] file?");
var answerGitIgnore = AnsiConsole.Confirm("Generate a [yellow].gitignore[/] file?");

var framework = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Select [green]test framework[/] to use")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more frameworks)[/]")
        .AddChoices(new[] {
            "XUnit", "NUnit","MSTest"
        }));

// AnsiConsole.MarkupLine(string.Format("[white]Generate README: {0}[/]", answerReadme ? "yes" : "no"));
// AnsiConsole.MarkupLine(string.Format("[white]Generate .gitignore: {0}[/]", answerGitIgnore ? "yes" : "no"));
// AnsiConsole.MarkupLine($"[white]Selected test framework[/] [blue]{framework}[/]!");

AnsiConsole.Status()
    .Start("Generating project...", ctx =>
    {
      if(answerReadme) 
      {
        AnsiConsole.MarkupLine("LOG: Creating README ...");
        Thread.Sleep(1000);
        // Update the status and spinner
        ctx.Status("Next task");
        ctx.Spinner(Spinner.Known.Star);
        ctx.SpinnerStyle(Style.Parse("green"));
      }

      if(answerGitIgnore) 
      {
        AnsiConsole.MarkupLine("LOG: Creating .gitignore ...");
        Thread.Sleep(1000);
        // Update the status and spinner
        ctx.Status("Next task");
        ctx.Spinner(Spinner.Known.Star);
        ctx.SpinnerStyle(Style.Parse("green"));
      }
      
      // Simulate some work
      AnsiConsole.MarkupLine("LOG: Configuring test framework...");
      Thread.Sleep(2000);
    });