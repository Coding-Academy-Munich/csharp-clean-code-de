using System.Text.Json;

namespace AdventureData
{
    public static class JsonLoader
    {
        public static List<Dictionary<string, object?>> LoadData(string fileName)
        {
            var filePath = FileFinder.Find(fileName);
            try
            {
                using var stream = File.OpenRead(filePath);
                return ParseJson(stream);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error loading file '{fileName}': {e.Message}");
                return new List<Dictionary<string, object?>>();
            }
        }

        public static List<Dictionary<string, object?>> ParseJson(string jsonString)
        {
            try
            {
                using var document = JsonDocument.Parse(jsonString);
                return ParseElement(document.RootElement);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error parsing JSON: {e.Message}");
                return new List<Dictionary<string, object?>>();
            }
        }

        public static List<Dictionary<string, object?>> ParseJson(Stream inputStream)
        {
            try
            {
                using var document = JsonDocument.Parse(inputStream);
                return ParseElement(document.RootElement);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error parsing JSON stream: {e.Message}");
                return new List<Dictionary<string, object?>>();
            }
        }

        // Parses the root element of the JSON document
        private static List<Dictionary<string, object?>> ParseElement(JsonElement rootElement)
        {
            if (rootElement.ValueKind != JsonValueKind.Array)
            {
                throw new ArgumentException("Expected a JSON array as the root element.");
            }

            var result = new List<Dictionary<string, object?>>();
            foreach (var jsonElement in rootElement.EnumerateArray())
            {
                result.Add(ParseObject(jsonElement));
            }

            return result;
        }

        // Parses a single dictionary-like object from JsonElement
        private static Dictionary<string, object?> ParseObject(JsonElement element)
        {
            var dict = new Dictionary<string, object?>();

            foreach (var property in element.EnumerateObject())
            {
                dict[property.Name] = ParseValue(property.Value);
            }

            return dict;
        }

        // Recursively parses a JsonElement into an appropriate .NET type
        private static object? ParseValue(JsonElement value)
        {
            return value.ValueKind switch
            {
                JsonValueKind.Object => ParseObject(value),
                JsonValueKind.Array => ParseArray(value),
                JsonValueKind.String => value.GetString(),
                JsonValueKind.Number => value.TryGetInt64(out long l) ? l : value.GetDouble(),
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                JsonValueKind.Null => null,
                JsonValueKind.Undefined => null,
                _ => throw new ArgumentException($"Unsupported JSON value type: {value.ValueKind}"),
            };
        }

        // Parses a JsonElement array into a C# list of objects
        private static List<object?> ParseArray(JsonElement arrayElement)
        {
            var list = new List<object?>();
            foreach (var item in arrayElement.EnumerateArray())
            {
                list.Add(ParseValue(item));
            }

            return list;
        }
    }
}
