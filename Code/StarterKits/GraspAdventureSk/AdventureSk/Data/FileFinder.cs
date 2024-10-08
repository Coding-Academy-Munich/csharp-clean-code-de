namespace AdventureSk.Data;

using System.IO;

public static class FileFinder
{
    public static string Find(string name)
    {
        return Directory.GetFiles(Directory.GetCurrentDirectory(), name, SearchOption.AllDirectories)
            .FirstOrDefault() ?? throw new FileNotFoundException($"File {name} not found");
    }
}
