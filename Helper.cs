namespace Helper;
using Spectre.Console;
using System.Reflection;
using Actions;

public class Helpers 
{
  public Helpers()
  {
    CwdPath = Directory.GetCurrentDirectory();
    string toolRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    string baseToolPath = new Uri(toolRoot + "/../../../").LocalPath;
    TemplatePath = Path.Combine(baseToolPath, "templates");

  }
  public string CwdPath { get; set; }
  public string TemplatePath { get; set; }
  public bool CreateReadme { get; set; }
  public bool CreateGitIgnore { get; set; }
  public string SelectedFramework { get; set; }
  enum MessageTypeEnum 
  {
    Log,
    Error,
    Information,
    Success,
    Warning
  }
  static Dictionary<MessageTypeEnum,string> MessageTypes = new Dictionary<MessageTypeEnum, string>() 
  {
    [MessageTypeEnum.Log] ="white" ,
    [MessageTypeEnum.Error]="red",
    [MessageTypeEnum.Information]="blue",
    [MessageTypeEnum.Success]="green",
    [MessageTypeEnum.Warning]="yellow"
  };
  public static void Log(string message) => Write(message, MessageTypes[MessageTypeEnum.Log]);
  public static void Error(string error) => Write(error, MessageTypes[MessageTypeEnum.Error]);
  public static void Information(string information) => Write(information, MessageTypes[MessageTypeEnum.Information]);
  public static void Success(string success) => Write(success, MessageTypes[MessageTypeEnum.Success]);
  public static void Warning(string warning) => Write(warning, MessageTypes[MessageTypeEnum.Warning]);

  private static void Write(string message, string color)
  {
    AnsiConsole.MarkupLine(string.Format("[{0}]{1}[/]", color, message));
  }

  public static void WriteHeader(string header)
  {
    AnsiConsole.Write(
    new FigletText(header)
        .LeftAligned()
        .Color(Color.Red));
  }

  public void RunProgram()
  {
    WriteHeader("Scaffold-demo");

    Console.WriteLine("");
    CreateReadme = AnsiConsole.Confirm("Generate a [green]README[/] file?");
    CreateGitIgnore = AnsiConsole.Confirm("Generate a [yellow].gitignore[/] file?");

    SelectedFramework = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Select [green]test framework[/] to use")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more frameworks)[/]")
        .AddChoices(new[] {
            "XUnit", "NUnit","MSTest"
        }));

    AnsiConsole.Status()
    .Start("Generating project...", ctx =>
    {
      if (CreateReadme)
      {
        Action.CreateFile(CwdPath, TemplatePath, "README.md");
        Thread.Sleep(1000);
        ctx.Status("Next task");
        ctx.Spinner(Spinner.Known.Star);
        ctx.SpinnerStyle(Style.Parse("green"));
      }

      if (CreateGitIgnore)
      {
        Action.CreateFile(CwdPath, TemplatePath, ".gitignore");

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
  }
}