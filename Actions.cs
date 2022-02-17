using Spectre.Console;
using System.IO;

namespace Actions;

public static class Action 
{
  public static void CreateFile(string path, string templatePath, string fileName) 
  {
    var dest = Path.Combine(path, fileName);
    var src = Path.Combine(templatePath, fileName);
    AnsiConsole.MarkupLine(string.Format("LOG: Creating {0} ...", fileName));
    if (File.Exists(dest))
    {
      Helper.Helpers.Warning(string.Format("IGNORE: File already exist"));
    }
    else
    {
      File.Copy(src, dest);
      Helper.Helpers.Success(string.Format("CREATED: File '{0}' created", fileName));
    }
  }
}