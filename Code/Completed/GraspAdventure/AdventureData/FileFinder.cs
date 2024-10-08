using System.IO;

namespace AdventureData;

public static class FileFinder
{
    public static string Find(string name)
    {
        string startPath = Path.GetFullPath(".");
        var files = Directory.EnumerateFiles(startPath, name, SearchOption.AllDirectories);
        return files.FirstOrDefault()
            ?? throw new FileNotFoundException($"File '{name}' not found in '{startPath}'.");
    }
}