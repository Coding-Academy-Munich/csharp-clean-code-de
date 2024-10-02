using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

public class FileFinder
{
    public static string Find(string name)
    {
        var directoryFiles = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*", SearchOption.AllDirectories);
        return directoryFiles.FirstOrDefault(f => Path.GetFileName(f).Equals(name))
               ?? throw new FileNotFoundException($"File '{name}' not found");
    }
}

public class JsonLoader
{
    public static List<Dictionary<string, object>> LoadData(string fileName)
    {
        try
        {
            string filePath = FileFinder.Find(fileName);
            string jsonContent = File.ReadAllText(filePath);
            var simpleLocations = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonContent);
            return simpleLocations ?? new List<Dictionary<string, object>>();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading JSON data: {e.Message}");
            return new List<Dictionary<string, object>>();
        }
    }
}