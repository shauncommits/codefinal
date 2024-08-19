using System.Text.Json;

using CodeAPI.Models;

namespace CodeAPI.Controllers;

public class JSONHandler
{
    public static List<CodeContent> ReadJSONFromFileAsCarList()
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var json = File.ReadAllText("./Data/CodeData.json");
        List<CodeContent> codeContents = JsonSerializer.Deserialize<List<CodeContent>>(json, options);
        return codeContents;
    }
}