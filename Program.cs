using Spectre.Console;
using LibGit2Sharp;
using Newtonsoft.Json;

namespace webdev;

class Program
{
    static void Main(string[] args)
    {
        var command = args[0];

        if (string.IsNullOrEmpty(command))
        {
            throw new ArgumentNullException("There was no command passed to webdev");
        }

        switch (command)
        {

        }
    }

    static void OpenProject(string path)
    {
        var project = JsonConvert.DeserializeObject<Project>(path);
        project.Initialize();

        while (true)
        {
            AnsiConsole.Write($"[green]{project.Name}[/]~>");
            var line = Console.ReadLine();

            switch (line)
            {
                case "add-component":
                    break;
            }
        }
    }

    static void CreateProject(string[] args)
    {

    }
}

public class Project
{
    public string Name { get; set; }
    public string Path { get; set; }
    public List<IComponent> Sections { get; set; }
    public List<IComponent> Components { get; set; }
    public List<ICssVariable> CssVariables { get; set; }

    public void CreateBoilerPlate()
    {
        var directories = new[]
        {
            "php",
            "php\\components",
            "php\\sections",
            "css",
            "css\\layout",
            "assets",
            "scripts"
        };

        var files = new[]
        {
            "index.php",
            "header.php",
            "footer.php",
            "css\\style.scss",
            "css\\layout\\layout.scss",
        };

        Array.ForEach(directories, dir =>
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        });

        Array.ForEach(files, file =>
        {
            if (!File.Exists(file))
            {
                File.Create(file);
            }
        });
    }

    public Repository Initialize()
    {
        return new Repository(Path);
    }

    public Project(string name, string path)
    {
        Name = name;
        Path = path;

        Sections = new List<IComponent>();
        Components = new List<IComponent>();
        CssVariables = new List<ICssVariable>();

        CreateBoilerPlate();
        Initialize();
    }

    public static Project Create(string name, string path)
    {
        return new Project(name, path);
    }
}

public interface ICssVariable
{
    public string Name { get; set; }
    public object Value { get; set; }
}

public interface IComponent
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string ScriptPath { get; set; }
    public string CssPath { get; set; }
}

